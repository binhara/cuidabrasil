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
using System;

namespace AppFVCShared.Services
{
    public interface IBaseModel
    {
        string Id { get; set; }
    }
    //[Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public abstract class BaseModel : IBaseModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
