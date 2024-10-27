﻿namespace Product_Rent.Models
{
    public class Caixa
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public DateTime Data { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoFinal { get; set; }
        public decimal TotalRecebimentos { get; set; }
        public decimal TotalRetiradas { get; set; }
        public int FuncionarioId { get; set; }
    }
}
