using Microsoft.AspNetCore.Mvc;
using PuzzleAPI.Interfaces;
using PuzzleAPI.Models;
using System;

namespace PuzzleAPI.Controllers
{
    [ApiController]
    [Route("api/Puzzle")]
    public class AnswersController : Controller
    {
        // allows access to the DAL
        IAnswers _Service;
        public AnswersController(IAnswers service)
        {
            _Service = service;
        }

        // gets answers to specified question
        [HttpGet]
        [Route("GetAnswers")]
        public IActionResult GetAnswers(int questionId)
        {
            try
            {
                var result = _Service.GetAnswers(questionId);
                if (result == null) return NotFound();
                return Ok(result);


            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // gets specified answer
        [HttpGet]
        [Route("GetAnswer")]
        public IActionResult GetAnswer(int answerId)
        {
            try
            {
                var result = _Service.GetAnswer(answerId);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // creates answer
        [HttpPost]
        [Route("AddAnswer")]
        public void AddAnswer(string Answer, int questionId, bool isCorrect)
        {
            try
            {
                _Service.AddAnswer(Answer, questionId, isCorrect);
            }
            catch (Exception)
            {
            }
        }

        // deletes answer
        [HttpDelete]
        [Route("DeleteAnswer")]
        public void DeleteAnswer(int answerId)
        {
            try
            {
                var result = _Service.GetAnswer(answerId);
                if (result != null)
                {
                    _Service.DeleteAnswer(answerId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // updates answer
        [HttpPost]
        [Route("UpdateAnswer")]
        public void UpdateAnswer(Answers answer)
        {
            try
            {
                _Service.UpdateAnswer(answer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // checks user submitted answers and assigns points
        [HttpPost]
        [Route("SubmitAnswer")]
        public IActionResult SubmitAnswer(string appId, AnswersSubmitted answersSubmitted, bool passed)
        {
            try
            {
                var result = _Service.SubmitAnswer(appId, answersSubmitted, passed);
                if (result == null) return NotFound();
                return Ok("User Got " + result + " Points.");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // checks user submitted string and assigns points
        [HttpPost]
        [Route("SubmitTextAnswer")]
        public IActionResult SubmitTextAnswer(string appId, string textAnswer, AnswersSubmitted answersSubmitted, bool passed)
        {
            try
            {
                var result = _Service.SubmitTextAnswer(appId, textAnswer, answersSubmitted, passed);
                if (result == null) return NotFound();
                return Ok("User Got " + result + " Points.");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
