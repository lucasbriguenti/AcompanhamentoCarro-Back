﻿using System;

namespace OnboardingSIGDB1.Domain.Dto
{
    public class EmpresaDto
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public DateTime? DataFundacao { get; set; }
    }
}
