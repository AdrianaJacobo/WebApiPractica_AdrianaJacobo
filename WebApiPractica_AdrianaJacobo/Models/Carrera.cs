﻿using System;
using System.Collections.Generic;

namespace WebApiPractica_AdrianaJacobo.Models;

public partial class Carrera
{
    public int CarreraId { get; set; }

    public string? NombreCarrera { get; set; }

    public int? FacultadId { get; set; }
}
