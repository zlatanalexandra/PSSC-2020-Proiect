﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Access.Primitives.IO.Mocking;
using LanguageExt;

namespace Access.Primitives.IO
{
    public abstract class Adapter<O, R> : Adapter<O, R, object, object> { }

    public abstract class Adapter<O, R, S, D> : IInterpreter<S, D>, IAdapter<O, R>
    {
        public virtual Task<(R, bool)> ShouldExecute(S state) => Task.FromResult((default(R), true));
        protected Port<O, R, A> Cast<A>(Port<A> ma) => (Port<O, R, A>)ma;


        protected virtual async Task<R> AdapterSpecificLogic(O cmd, S state, D dependencies)
        {
            var shouldExecute = await ShouldExecute(state);
            if (shouldExecute.Item2)
            {
                return await Work(cmd, state, dependencies);
            }
            else
            {
                return shouldExecute.Item1;
            }
        }

        protected virtual Task AdapterSpecificAssertions(S state, O cmd, R result) => Task.CompletedTask;

        public async Task<A> Interpret<A>(Port<A> ma, object state, object dependencies, Func<Port<A>, Task<A>> interpret)
        {
            var op = Cast(ma).Cmd;
            var result = await AdapterSpecificLogic(op, (S)state, (D)dependencies);
            await PostConditions(op, result, (S)state);
            return await interpret(Cast(ma).Do(result));
        }

        public abstract Task PostConditions(O cmd, R result, S state);

        public async Task<A> Mock<A>(MockContext mockContext, Port<A> ma, S state, D dependencies, Func<Port<A>, Task<A>> interpret)
        {
            mockContext.AttributeDiscovery(this);
            var op = Cast(ma).Cmd;
            var result = await AdapterSpecificLogic(op, state, dependencies);
            await Assertions(mockContext.GetAll(), Cast(ma).Cmd, result, state, dependencies);
            await AdapterSpecificAssertions(state, op, result);
            return await interpret(Cast(ma).Do(result));
        }

        public abstract Task<R> Work(O cmd, S state, D dependencies);

        public virtual Task Assertions(object[] path, O cmd, R result, S state, D dependencies) => throw new Exception("Override the 'Assertions' method in your Adapter and provide some Asserts in order to have the tests properly working");

    }
}