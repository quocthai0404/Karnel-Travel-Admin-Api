using System;
using System.Collections.Generic;

namespace Karnel_Travel_Admin_Api.Models;

public partial class WordType
{
    public int WordTypeId { get; set; }

    public string WordTypeName { get; set; } = null!;

    public virtual ICollection<Word> Words { get; set; } = new List<Word>();
}
