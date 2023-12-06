using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        public long Incluir(DML.Beneficiario cliente)
        {
            DAL.Clientes.DaoBeneficiario cli = new DAL.Clientes.DaoBeneficiario();
            return cli.Incluir(cliente);
        }


        public List<DML.Beneficiario> Listar(long IDCLIENTE)
        {
            DAL.Clientes.DaoBeneficiario cli = new DAL.Clientes.DaoBeneficiario();
            return cli.Listar(IDCLIENTE);
        }


        public bool VerificarExistenciaBeneficiario(string CPF)
        {
            DAL.Clientes.DaoBeneficiario cli = new DAL.Clientes.DaoBeneficiario();
            return cli.VerificarExistenciaBeneficiario(CPF);
        }
    }
}
