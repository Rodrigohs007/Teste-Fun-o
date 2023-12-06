using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using WebAtividadeEntrevista.Function;
using System.Reflection;

namespace WebAtividadeEntrevista.Controllers
{
    
    public class ClienteController : Controller
    {

        Function.ValidaCPF _ValidaCPF = new Function.ValidaCPF();

        bool VerificaCPF = false;
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            
                return View();
           
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            

            BoCliente bo = new BoCliente();
            
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                if (_ValidaCPF.IsCpf(model.CPF))
                {
                    VerificaCPF =  bo.VerificarExistencia(model.CPF);

                    if(VerificaCPF != true)
                    {
                        model.Id = bo.Incluir(new Cliente()
                        {
                            CEP = model.CEP,
                            Cidade = model.Cidade,
                            Email = model.Email,
                            Estado = model.Estado,
                            Logradouro = model.Logradouro,
                            Nacionalidade = model.Nacionalidade,
                            Nome = model.Nome,
                            Sobrenome = model.Sobrenome,
                            Telefone = model.Telefone,
                            CPF = model.CPF,
                        });

                        ViewBag.Id_Cliente = model.Id;


                        return Json("Cadastro efetuado com sucesso");
                    }

                    else
                    {
                        return Json("CPF já cadastrado");
                    }

                    

                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "CPF Inválido" });
                }
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model, string Ben_NOME, string Ben_CPF)
        {
            BoCliente bo = new BoCliente();
            BoBeneficiario boBeneficiario = new BoBeneficiario();
       
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                if (!String.IsNullOrEmpty(Ben_NOME) && !String.IsNullOrEmpty(Ben_CPF))
                {
                    if (_ValidaCPF.IsCpf(Ben_CPF))
                    {

                        VerificaCPF = boBeneficiario.VerificarExistenciaBeneficiario(Ben_CPF);
                        if (VerificaCPF != true)
                        {

                            boBeneficiario.Incluir(new Beneficiario()
                            {
                                NOME = Ben_NOME,
                                CPF = Ben_CPF,
                                IDCLIENTE = model.Id

                            });

                            return Json("Cadastro alterado com sucesso");
                        }

                        else
                        {
                            return Json("CPF já cadastrado");
                        }
                    }

                    else
                    {
                        return Json("CPF Inválido");
                    }
                }


                if (_ValidaCPF.IsCpf(model.CPF))
                {
                    VerificaCPF = bo.VerificarExistencia(model.CPF);

                    if (VerificaCPF != true)
                    {
                        bo.Alterar(new Cliente()
                        {
                            Id = model.Id,
                            CEP = model.CEP,
                            Cidade = model.Cidade,
                            Email = model.Email,
                            Estado = model.Estado,
                            Logradouro = model.Logradouro,
                            Nacionalidade = model.Nacionalidade,
                            Nome = model.Nome,
                            Sobrenome = model.Sobrenome,
                            Telefone = model.Telefone,
                            CPF = model.CPF
                        });
                               
                        return Json("Cadastro alterado com sucesso");

                    }

                    else
                    {
                        return Json("CPF já cadastrado");
                    }

                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "CPF Inválido" });
                }
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id, string Nome, string CPF)
        {
            BoCliente bo = new BoCliente();
            BoBeneficiario boBeneficiario = new BoBeneficiario();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    CPF = cliente.CPF
                };

                ViewBag.Id_Cliente = true;
                ViewBag.Id = cliente.Id;

                
                

            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }




        public ActionResult Cad_Beneficiaros(int Id)
        {
            return Json("CPF já cadastrado");
        }
    }
}