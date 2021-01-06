using Access.Primitives.IO;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion;
using StackUnderflow.EF.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackUnderflow.Domain.Core.Contexts.Question;
using LanguageExt;
using StackUnderflow.Domain.Core.Contexts;
using StackUnderflow.Domain.Schema.Backoffice.CreateTenantOp;
using StackUnderflow.Domain.Schema.Backoffice.InviteTenantAdminOp;
using System.Linq;
using System;
using StackUnderflow.Domain.Core.Contexts.Question.SendAckToOwner;
using Access.Primitives.EFCore;
using Orleans;

namespace StackUnderflow.API.AspNetCore.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly DatabaseContext _dbContext;
        private readonly IClusterClient _Client;

        public QuestionsController(IInterpreterAsync interpreter,
                                   DatabaseContext dbContext,
                                   IClusterClient Client)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;
            _Client = Client;
        }
        [HttpPost("createQuestionAgain")]
        public async Task<IActionResult> CreateQuestion()
        {
            //QuestionWriteContext ctx = new QuestionWriteContext(
                 // new EFList<Tenant>(_dbContext.Question),
                 // new EFList<TenantUser>(_dbContext.Question),
                 //new EFList<User>(_dbContext.User));
                 //);
            var stream = _Client.GetStreamProvider("SMSProvider").GetStream<Post>(Guid.Empty, "questions");
            var post = new Post
            {
                PostId = 2,
                PostText = "My question2"
            };

            await stream.OnNextAsync(post);
            return Ok();
        }
        [HttpPost("createQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestion cmd)
        {
            var dep = new QuestionDependencies();

            // var questions = await _dbContext.QuestionModel.ToListAsync();

            // var ctx = new QuestionWriteContext(questions);

            //.QuestionModel.AttachRange(questions);
            var ctx = new QuestionWriteContext(new EFList<QuestionModel>(_dbContext.QuestionModel));

            var expr = from CreateQuestionResult in QuestionDependencies.CreateQuestion(createQuestionCmd)
                           //let checkLanguageCmd = new CheckLanguage()
                           //select CreateQuestionResult;
                           //from checkLanguageResult in QuestionContext.CheckLanguage(new CheckLanguageCmd())
                       from sendAckToQuestionOwnerCmd in QuestionContext.SendAckToOwner(new SendAckToOwnerCmd(1, 2))
                       select CreateQuestionResult;

            var r = await _interpreter.Interpret(expr, ctx, dep);


            //dbContext.QuestionModel.Add(new DatabaseModel.Models.QuestionModel { QuestionId = Guid.NewGuid(), Title = cmd.Title, Description = cmd.Description, Tags = cmd.Tags });
            //var reply = await _dbContext.QuestionModel.Where(r => r.Title == "Intrebarea1").SingleOrDefaultAsync();
            //_dbContext.QuestionModel.Update(reply);
            // await _dbContext.SaveChangesAsync();

            return r.Match(
                succ => (IActionResult)Ok("Successfully"),
                fail => BadRequest("Reply could not be added")
                );
        }
    }

    internal class DatabaseContext
    {
    }
}