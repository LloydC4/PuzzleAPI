using Microsoft.AspNetCore.Mvc;
using PuzzleAPI.Interfaces;
using PuzzleAPI.Models;
using System;

namespace PuzzleAPI.Controllers
{
    [ApiController]
    [Route("api/Puzzle")]
    public class QuestionsController : Controller
    {
        // allows access to the DAL
        IQuestions _Service;
        public QuestionsController(IQuestions service)
        {
            _Service = service;
        }
        // gets all questions and answers
        [HttpGet]
        [Route("GetQuestions")]
        public IActionResult GetQuestions()
        {
            try
            {
                var result = _Service.GetQuestions();
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // deletes specified question
        [HttpDelete]
        [Route("DeleteQuestion")]
        public void DeleteQuestion(int questionId)
        {
            try
            {
                _Service.DeleteQuestion(questionId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // gets specified question and answers
        [HttpGet]
        [Route("GetQuestion")]
        public IActionResult GetQuestion(int questionId)
        {
            try
            {
                var result = _Service.GetQuestion(questionId);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // creates new question
        [HttpPost]
        [Route("AddQuestion")]
        public void AddQuestion(string questionText, string hint, int questionCategory)
        {
            try
            {
                _Service.AddQuestion(questionText, hint, questionCategory);
            }
            catch (Exception)
            {
            }
        }
        // updates specified question
        [HttpPost]
        [Route("UpdateQuestion")]
        public void UpdateQuestion(Questions question)
        {
            try
            {
                _Service.UpdateQuestion(question);
            }
            catch (Exception)
            {
            }
        }
        // creates quiz for user
        [HttpGet]
        [Route("GenerateQuiz")]
        public IActionResult GenerateQuiz(int categoryId, int numberOfQuestions, string appId)
        {
            try
            {
                var result = _Service.GenerateQuiz(categoryId, numberOfQuestions, appId);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // gets the next question in the quiz, returns same question if previous question not answered correctly or passed
        [HttpGet]
        [Route("GetUserQuestion")]
        public IActionResult GetUserQuestion(int userId)
        {
            try
            {
                var result = _Service.GetUserQuestion(userId);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
