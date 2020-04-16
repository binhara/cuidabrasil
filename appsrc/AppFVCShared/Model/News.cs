//
// Journal.cs: Assignments.
//
// Author:
//      Adriano D'Luca Binhara Gonçalves (adriano@azuris.com.br)
//	    Carol Yasue (carolina_myasue@hotmail.com)
//
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//

namespace AppFVCShared.Model
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Uri { get; set; }
        public string PhoneNumber { get; set; }
    }
}
