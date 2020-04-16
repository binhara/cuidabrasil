//
// Journal.cs: Assignments.
//
// Author:
//      Alessandro de Oliveira Binhara (binhara@azuris.com.br)
//
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//
using FCVLibWS;

namespace AppFVCShared.WebService
{
    public class BaseWs
    {
        protected Client ObjClient;
        protected SuccessfulAnswer ObjSuccessfulAnswer;
        public BaseWs(Client objClient)
        {
            ObjClient = objClient;
        }
    }
}
