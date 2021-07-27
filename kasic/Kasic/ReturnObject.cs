using System;
using System.Collections.Generic;
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
            this.command = command;
            switch (command.CommandSettings.ReturnType)
            {
                case KasicType.VOID:
                    this.toReturn = null;
                    break;
                default:
                    Panic(toReturn); break;
            }
        }

        public ReturnObject(Command command, object toReturn)
        {
            this.command = command;
            switch (Type.GetTypeCode(toReturn.GetType()))
            {
                case TypeCode.Double:
                    if (command.CommandSettings.ReturnType != KasicType.NUMBER)
                        Panic(toReturn);
                    this.toReturn = toReturn;
                    break;
                case TypeCode.Boolean:
                    if (command.CommandSettings.ReturnType != KasicType.BOOL)
                        Panic(toReturn);
                    this.toReturn = toReturn;
                    break;
                default: // string
                    this.toReturn = toReturn;
                    break;
            }
        }

        public KasicObject AsKasicObject()
        {
            return new KasicObject(toReturn, command.CommandSettings.ReturnType);
        }

        public override string ToString()
        {
            return toReturn == null ? "" : toReturn.ToString();
        }
        
        private void  Panic(object toReturn)
        {
            throw new ArgumentException($"The value {toReturn} does not match {command.CommandSettings.ReturnType}");
        }
    }
}