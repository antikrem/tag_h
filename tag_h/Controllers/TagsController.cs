﻿using System.Collections.Generic;
using System.Linq;

using EphemeralEx.Extensions;
using Microsoft.AspNetCore.Mvc;

using tag_h.Core.Model;
using tag_h.Core.Repositories;
using tag_h.Injection.Typing;


namespace tag_h.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        [Route("[action]")]
        [ClientReturns(typeof(IEnumerable<Tag>))]
        public TagSet GetAllTags()
        {
            return _tagRepository.GetAllTags();
        }

        [HttpGet]
        [Route("[action]")]
        public Tag CreateTag(string name)
        {
            return _tagRepository.CreateTag(name, name.ToLower().ToEnumerable());
        }

        //TODO, Remove, tag values move with GetAllTags
        [HttpGet]
        [Route("[action]")]
        public List<string> GetValues(int tagId)
        {
            return _tagRepository
                .GetAllTags()
                .Where(tag => tag.Id == tagId)
                .First()
                .Values
                .ToList();
        }
    }
}
