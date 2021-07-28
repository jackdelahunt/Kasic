using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using kasic.Commands;
using kasic.Utils;
using OperationResult;

namespace kasic.Kasic
{
    public class ReturnObject : IReturnObject
    {
        private object toReturn;
        private Command command;
        
        public ReturnObject(Command command)
        {
            Debug.Assert(command.CommandSettings.ReturnType == KasicType.VOID);
            this.command = command;
        }

        public ReturnObject(Command command, double num)
        {
            Debug.Assert(command.CommandSettings.ReturnType == KasicType.NUMBER);
            this.command = command;
            this.toReturn = num;
        }
        
        public ReturnObject(Command command, bool boolean)
        {
            Debug.Assert(command.CommandSettings.ReturnType == KasicType.BOOL);
            this.command = command;
            this.toReturn = boolean;
        }
        
        public ReturnObject(Command command, string str)
        {
            Debug.Assert(command.CommandSettings.ReturnType == KasicType.STRING);
            this.command = command;
            this.toReturn = str;
        }

        public KasicObject AsKasicObject()
        {
            return new KasicObject(toReturn, command.CommandSettings.ReturnType, isNative:false);
        }

        public override string ToString()
        {
            return toReturn == null ? "" : toReturn.ToString();
        }
    }
}