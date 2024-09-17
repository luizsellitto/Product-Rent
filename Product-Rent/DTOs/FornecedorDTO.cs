﻿using Product_Rent.Models;
using System.ComponentModel.DataAnnotations;

namespace Product_Rent.DTOs
{
    public class FornecedorDTO
    {
        public int Id { get; set; }

        [Required]
        public string CNPJ { get; set; }

        [Required]
        public string RazaoSocial { get; set; }

        [Required]
        public string NomeFantasia { get; set; }

        [Required]
        public string InscricaoEstadual { get; set; }

        [Required]
        public string InscricaoMunicipal { get; set; }

        [Required]
        public string Responsavel { get; set; }

        [Required]
        public List<Endereco> Endereco { get; set; }

        [Required]
        public string ContatoUm { get; set; }

        public string ContatoDois { get; set; }

        public string ContatoTres { get; set; }

        [Required]
        public Endereco EmailUm { get; set; }

        public Endereco EmailDois { get; set; }
    }
}