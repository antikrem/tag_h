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

        // List of tags that are associated with this field
        public List<Tag> Tags { get; }

        // Generates a new tfield
        public Field(string fieldName, List<Tag> tags)
        {
            this.Name = fieldName;
            this.Tags = tags;
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
        //  List of root fields
        private List<Field> roots = new List<Field>();

        // Takes a XmlNode of tag and returns the tag
        Tag parseTag(XmlNode node)
        {
            Tag tag = new Tag(
                    node.Attributes["name"].Value,
                    parseFields(node.ChildNodes)
                );

            return tag;

        }

        // Takes ChildNodeList of tags and returns a list of tags
        List<Tag> parseTags(XmlNodeList nodes)
        {
            List<Tag> tags = new List<Tag>();

            foreach (XmlNode tagNode in nodes)
            {
                tags.Add(parseTag(tagNode));
            }

            return tags;
        }

        // Takes a single XMLNode of field and returns 
        Field parseField(XmlNode node)
        {
            Field field = new Field(
                    node.Attributes["name"].Value,
                    parseTags(node.ChildNodes)
                );

            return field;

        }

        // Takes a ChildNodeList of fields
        // And creates a List of these fields
        List<Field> parseFields(XmlNodeList nodes)
        {
            List<Field> fields = new List<Field>();

            foreach (XmlNode fieldNode in nodes)
            {
                fields.Add(parseField(fieldNode));
            }

            return fields;
        }

        // Generate a Tagstructure from file
        public TagStructure(string fileLocation)
        {
            XmlDocument tagFile = new XmlDocument();
            tagFile.Load(fileLocation);
            this.roots = parseFields(tagFile.ChildNodes[1].ChildNodes);
        }

        // Specifies this field and its children is not selected
        private void markFieldNotSelected(Field field)
        {
            field.IsActive = false;
            foreach (Tag tag in field.Tags)
            {
                tag.IsSelected = false;
                tag.Fields.ForEach(subField => markFieldNotSelected(subField));

            }
        }

        // Applies list of tags to this field
        private void markField(List<string> tags, Field field)
        {
            field.IsActive = true;
            foreach (Tag tag in field.Tags)
            {
                if (tags.Contains(tag.Name))
                {
                    // Set this tag as selected
                    tag.IsSelected = true;
                    // Propogate tag forward
                    tag.Fields.ForEach(subField => markField(tags, subField));
                }
                else
                {
                    // Set this tag as not selected
                    tag.IsSelected = false;
                    // Propogate no tag forward
                    tag.Fields.ForEach(subField => markFieldNotSelected(subField));
                }
            }
        }

        // Marks this TagStructure with a given list of tags
        public void markWithTags(List<string> tags)
        {
            this.roots.ForEach(field => markField(tags, field));
        }

        // Gets roots
        public List<Field> getRoots()
        {
            return roots;
        }
    }
}
