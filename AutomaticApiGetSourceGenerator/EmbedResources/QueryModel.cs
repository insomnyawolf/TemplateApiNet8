﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ApiGetGenerator;
using ApiGetGenerator.Models;

namespace TemplateNamespaceTemplate;

public partial class TemplateQueryModelClassNameTemplate
{
    public List<OrderBy<TemplateDatabaseClassColumnsNameTemplate>> OrderBy { get; set; }
    public List<TemplateDatabaseClassIncludesNameTemplate> Includes { get; set; }
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
TemplateQueryModelContentTemplate
}