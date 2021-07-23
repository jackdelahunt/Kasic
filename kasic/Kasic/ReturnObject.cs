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

        public ReturnObject(Command command, double toReturn)
        {
            this.command = command;
            switch (command.CommandSettings.ReturnType)
            {
                case KasicType.NUMBER:
                    this.toReturn = toReturn;
                    break;
                default:
                    Panic(toReturn); break;
            }
        }
        
        public ReturnObject(Command command, bool toReturn)
        {
            this.command = command;
            switch (command.CommandSettings.ReturnType)
            {
                case KasicType.BOOL:
                    this.toReturn = toReturn;
                    break;
                default:
                    Panic(toReturn); break;
            }
        }
        
        public ReturnObject(Command command, string toReturn)
        {
            this.command = command;
            switch (command.CommandSettings.ReturnType)
            {
                case KasicType.ANY:
                case KasicType.STRING:
                    this.toReturn = toReturn;
                    break;
                default:
                    Panic(toReturn); break;
            }
        }

        public double AsNumber()
        {
            if (toReturn is double)
                return (double) toReturn;
            
            Panic(toReturn);
            return 0;
        }

        public bool AsBool()
        {
            if (toReturn is bool)
                return (bool) toReturn;
            
            Panic(toReturn);
            return false;
        }

        public string AsString()
        {
            if (toReturn is string)
                return (string) toReturn;
            
            Panic(toReturn);
            return "";
        }

        public string AsAny()
        {
            // as any casts the value to a string while
            // as string expects the value to be a string
            try
            {
                return toReturn.ToString();
            }
            catch
            {
                Panic(toReturn);
                return "";
            }
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