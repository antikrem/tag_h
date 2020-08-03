using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace tag_h
{
    /* Represents a field, which is a combination of tags */
    public class Field
    {
        // Set to true if this field is active
        public bool IsActive { get; set; }

        // Name of this field
        public string Name { get; }

        // An exclusive field can only have one tag active
        public bool Exclusive { get; }

        // List of tags that are associated with this field
        public List<Tag> Tags { get; }

        // Generates a new tfield
        public Field(string fieldName, List<Tag> tags, bool exclusive)
        {
            this.Name = fieldName;
            this.Tags = tags;
            this.Exclusive = exclusive;
        }


        // Marks this field with a single tag
        public void MarkWithTag(string tagger)
        {
            // If this field is exclusive, set already tagged fields to false
            Tags.ForEach(tag => tag.IsSelected = (tag.IsSelected && !Exclusive) || tag.Name == tagger);
        }

        // Unmarks this field with a single tag
        public void UnmarkWithTag(string tagger)
        {
            // If this field is exclusive, set already tagged fields to false
            Tags.ForEach(tag => tag.IsSelected = tag.IsSelected && tag.Name != tagger);
        }
    }

    /* Represents a tag, which is an element of a feild that can be active*/
    public class Tag
    {
        // Set to true if this tag is active
        public bool IsSelected { get; set; }

        // Name of this tag
        public string Name { get; }

        // List of fields that are dependent on this Field
        public List<Field> Fields { get; }

        // Generates a new tag
        public Tag(string tagName, List<Field> fields)
        {
            this.Name = tagName;
            this.Fields = fields;
        }

    }

    /* Represents the structure for tags for this application
     * This is dynamic and can be modified by user */
    class TagStructure
    {
        // List of root fields
        public List<Field> Roots { get; } = new List<Field>();

        // Location of underlying tag xml
        public string FileLocation { get; }

        // Takes a XmlNode of tag and returns the tag
        Tag ParseTag(XmlNode node)
        {
            Tag tag = new Tag(
                    node.Attributes["name"].Value,
                    ParseFields(node.ChildNodes)
                );

            return tag;

        }

        // Takes ChildNodeList of tags and returns a list of tags
        List<Tag> ParseTags(XmlNodeList nodes)
        {
            List<Tag> tags = new List<Tag>();

            foreach (XmlNode tagNode in nodes)
            {
                tags.Add(ParseTag(tagNode));
            }

            return tags;
        }

        // Takes a single XMLNode of field and returns 
        Field ParseField(XmlNode node)
        {
            Field field = new Field(
                    node.Attributes["name"].Value,
                    ParseTags(node.ChildNodes),
                    node.Attributes["exclusive"].Value == "true"
                );

            return field;

        }

        // Takes a ChildNodeList of fields
        // And creates a List of these fields
        List<Field> ParseFields(XmlNodeList nodes)
        {
            List<Field> fields = new List<Field>();

            foreach (XmlNode fieldNode in nodes)
            {
                fields.Add(ParseField(fieldNode));
            }

            return fields;
        }

        // Generate a Tagstructure from file
        public TagStructure(string fileLocation)
        {
            XmlDocument tagFile = new XmlDocument();
            tagFile.Load(fileLocation);
            this.Roots = ParseFields(tagFile.ChildNodes[1].ChildNodes);

            this.FileLocation = fileLocation;
        }

        // Recursivly adds a new field to current xml field
        public void SaveField(XmlWriter writer, Field field)
        {
            if (field is null)
            {
                return;
            }

            writer.WriteStartElement("field");
            writer.WriteAttributeString("name", field.Name);
            writer.WriteAttributeString("exclusive", field.Exclusive ? "true" : "false");

            foreach (Tag tag in field.Tags)
            {
                writer.WriteStartElement("tag");
                writer.WriteAttributeString("name", tag.Name);
                tag.Fields.ForEach(subField => SaveField(writer, subField));
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        // Saves tag structure to 
        public void SaveTagStructure()
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t"
            };

            XmlWriter writer = XmlWriter.Create(FileLocation, xmlWriterSettings);

            writer.WriteStartDocument();
            writer.WriteStartElement("roots");

            Roots.ForEach(field => SaveField(writer, field));

            writer.WriteEndDocument();
            writer.Close();
        }

        // Specifies this field and its children is not selected
        private void MarkFieldNotSelected(Field field)
        {
            field.IsActive = false;
            foreach (Tag tag in field.Tags)
            {
                tag.IsSelected = false;
                tag.Fields.ForEach(subField => MarkFieldNotSelected(subField));

            }
        }

        // Applies list of tags to this field
        private void MarkField(List<string> tags, Field field)
        {
            field.IsActive = true;
            foreach (Tag tag in field.Tags)
            {
                if (tags.Contains(tag.Name))
                {
                    // Set this tag as selected
                    tag.IsSelected = true;
                    // Propogate tag forward
                    tag.Fields.ForEach(subField => MarkField(tags, subField));
                }
                else
                {
                    // Set this tag as not selected
                    tag.IsSelected = false;
                    // Propogate no tag forward
                    tag.Fields.ForEach(subField => MarkFieldNotSelected(subField));
                }
            }
        }

        // Marks this TagStructure with a given list of tags
        public void MarkWithTags(List<string> tags)
        {
            this.Roots.ForEach(field => MarkField(tags, field));
        }
        
        // Gets tag as a list of strings
        public List<string> GetTagString()
        {
            Stack<Field> fields = new Stack<Field>();
            Roots.ForEach(x => fields.Push(x));
            List<string> tags = new List<string>();

            while (fields.Count > 0)
            {
                var field = fields.Pop();
                foreach (var tag in field.Tags)
                {
                    if (tag.IsSelected)
                    {
                        tags.Add(tag.Name);
                        fields.Extend(tag.Fields);
                    }
                }
            }

            return tags;
        }
 
    }
}
