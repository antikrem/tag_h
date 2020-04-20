using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace tag_h
{
    /* Represents a field, which is a combination of tags */
    class Field
    {
        // Name of this field
        private string fieldName;

        // List of tags that are associated with this field
        private List<Tag> tags = new List<Tag>();

        // Generates a new tfield
        public Field(string fieldName)
        {
            this.fieldName = fieldName;
        }

        // Add a new tag
        public void addTags(List<Tag> tags)
        {
            this.tags = tags;
        }
    }

    /* Represents a tag, which is an element of a feild that can be active*/
    class Tag
    {
        // Name of this tag
        private string tagName;

        // List of fields that are dependent on this Field
        private List<Field> fields = new List<Field>();

        // Generates a new tag
        public Tag(string tagName)
        {
            this.tagName = tagName;
        }

        // Add a dependent Field
        public void addFields(List<Field> fields)
        {
            this.fields = fields;
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
            Tag tag = new Tag(node.Attributes["name"].Value);

            // Add the dependent fields in this tag
            tag.addFields(parseFields(node.ChildNodes));

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
            Field field = new Field(node.Attributes["name"].Value);

            // Add tags in field
            field.addTags(parseTags(node.ChildNodes));

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

        public TagStructure(string fileLocation)
        {
            XmlDocument tagFile = new XmlDocument();
            tagFile.Load(fileLocation);
            var roots = parseFields(tagFile.ChildNodes[1].ChildNodes);
            
        }


    }
}
