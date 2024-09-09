namespace Product_Rent.Models
{
    public class ValidarCPF
    {
        static private string cpf;

        public static bool ValidaCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", ""); //remove pontos e hífen do CPF

            int soma = 0;


            if (cpf.Length != 11)
            { //verifica se o CPF possui 11 dígitos
                return false;
            }

            //calcula o primeiro dígito verificador

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (10 - i);//como o primeiro começa com 0 não precisa
                                                                //fazer o 10-, pq é 10 (já que esta é a 1ºparte
            }//multiplica em escadinha os números ([0]*10 , [1]*9, [2]*8...)
            int digito1 = soma % 11;
            if (digito1 < 2)
            {//se resto da divisão < 2, então digito1 tem que ser 0                                           
                digito1 = 0;
            }
            else
            {//se não, o digito tem que ser o a subtração de 11 - sobra                                     
                digito1 = 11 - digito1;
            }


            if (digito1 != int.Parse(cpf[9].ToString()))
            {//verifica se o primeiro dígito verificador é igual ao do CPF
                return false;
            }

            //calcula o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (11 - i);//mesma coisa do de cima, mas com 11 e não 10
                                                                //o motivo disso é que agora nós temos o D1
            }//multiplica em escadinha os números ([0]*11 , [1]*10, [2]*9...)
            int digito2 = soma % 11;//o % significa mod
            if (digito2 < 2)
            {//mesma coisa do digito1
                digito2 = 0;
            }

            else
            {
                digito2 = 11 - digito2;
            }

            //verifica se o segundo dígito verificador é igual ao do CPF
            if (digito2 != int.Parse(cpf[10].ToString()))
            {
                return false;
            }

            return true; //CPF válido
        }
    }
}
