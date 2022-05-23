using Microsoft.AspNetCore.Mvc;
using PuzzleAPI.Interfaces;
using PuzzleAPI.Models;
using System;

namespace PuzzleAPI.Controllers
{
    [ApiController]
    [Route("api/Puzzle")]
    public class QuestionCategoryController : Controller
    {
        // allows access to the DAL
        IQuestionCategory _Service;
        public QuestionCategoryController(IQuestionCategory service)
        {
            _Service = service;
        }
        // gets all question categories
        [HttpGet]
        [Route("GetQuestionCategories")]
        public IActionResult GetQuestionCategories()
        {
            try
            {
                var result = _Service.GetQuestionCategories();
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // gets specified question category
        [HttpGet]
        [Route("GetQuestionCategory")]
        public IActionResult GetQuestionCategory(int questionCategoryId)
        {
            try
            {
                var result = _Service.GetQuestionCategory(questionCategoryId);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // creates new question category
        [HttpPost]
        [Route("AddQuestionCategory")]
        public void AddQuestionCategory(string questionCategoryName)
        {
            try
            {
                _Service.AddQuestionCategory(questionCategoryName);
            }
            catch (Exception)
            {
            }
        }
        // deletes specified question category
        [HttpDelete]
        [Route("DeleteQuestionCategory")]
        public void DeleteQuestionCategory(int questionCategoryId)
        {
            try
            {
                var result = _Service.GetQuestionCategory(questionCategoryId);
                if (result != null)
                {
                    _Service.DeleteQuestionCategory(questionCategoryId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        // updates specified question category
        [HttpPost]
        [Route("UpdateQuestionCategory")]
        public void UpdateQuestionsCategory(QuestionCategory questionCategory)
        {
            try
            {
                _Service.UpdateQuestionCategory(questionCategory);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
