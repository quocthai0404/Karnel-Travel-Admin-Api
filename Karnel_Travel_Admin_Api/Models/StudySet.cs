﻿using System;
using System.Collections.Generic;

namespace Karnel_Travel_Admin_Api.Models;

public partial class StudySet
{
    public int StudySetId { get; set; }

    public string StudySetName { get; set; } = null!;

    public int FolderId { get; set; }

    public virtual Folder Folder { get; set; } = null!;

    public virtual ICollection<Word> Words { get; set; } = new List<Word>();
}
