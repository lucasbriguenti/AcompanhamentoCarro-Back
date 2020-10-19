using AutoMapper;
using FluentValidation;
using OnboardingSIGDB1.Data;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Models.Classes;
using System.Linq;

namespace OnboardingSIGDB1.Domain.Services.Funcionarios
{
    public class FuncionarioService : ServiceBase<Funcionario>, IFuncionarioService
    {
        private readonly IUnitOfWork<Empresa> _uowEmpresa;
        private readonly IUnitOfWork<FuncionarioCargo> _uowFuncionarioCargo;
        private readonly IUnitOfWork<Cargo> _uowCargo;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<FuncionarioCargo> _validadorFuncCargo;
        public FuncionarioService(IUnitOfWork<Funcionario> uow, 
            IUnitOfWork<FuncionarioCargo> uowFuncionarioCargo, 
            IUnitOfWork<Cargo> uowCargo,
            IUnitOfWork<Empresa> uowEmpresa,
            AbstractValidator<Funcionario> validator,
            AbstractValidator<FuncionarioCargo> validadorFuncCargo,
            NotificationContext notification,
            IMapper mapper) : base(uow, validator, notification)
        {
            _validadorFuncCargo = validadorFuncCargo;
            _uowFuncionarioCargo = uowFuncionarioCargo;
            _uowCargo = uowCargo;
            _uowEmpresa = uowEmpresa;
            _mapper = mapper;
        }

        public bool VincularCargo(VincularFuncionarioCargoDto dto)
        {
            var cargo = _uowCargo.Repositorio.Get(x => x.Id == dto.CargoId);
            var funcionario = _uow.Repositorio.Get(x => x.Id == dto.FuncionarioId);
            if (cargo != null && funcionario != null && !funcionario.FuncionarioCargos.Any(x => x.CargoId == cargo.Id) && funcionario.Empresa != null)
            {
                var funcionarioCargo = _mapper.Map<FuncionarioCargo>(dto);
                if(!funcionarioCargo.Validate(funcionarioCargo, _validadorFuncCargo))
                {
                    _notification.AddNotifications(funcionarioCargo);
                    return false;
                }

                _uowFuncionarioCargo.Repositorio.Adicionar(funcionarioCargo);
                return true;
            }
            else
            {
                _notification.AddNotification("0", "Impossivel vincular cargo");
                return false;
            }
        }

        public bool VincularEmpresa(int idFuncionario, int idEmpresa)
        {
            var funcionario = _uow.Repositorio.Get(x => x.Id == idFuncionario);
            var empresa = _uowEmpresa.Repositorio.Get(x => x.Id == idEmpresa);
            if (empresa != null && funcionario != null && funcionario.Empresa == null)
            {
                funcionario.Empresa = empresa;
                _uow.Repositorio.Atualizar(funcionario);
                return true;
            }
            else
            {
                _notification.AddNotification("Vinculação", "Erro na vinculação a empresa");
                return false;
            }
        }
        public override bool Armazenar(Funcionario funcionario, int? id = null)
        {
            var funcionarioRegistrado = _uow.Repositorio.Get(x => x.Cpf.Equals(funcionario.Cpf.LimpaMascaraCnpjCpf()));
            if (funcionarioRegistrado != null && id == null)
            {
                _notification.AddNotification("0", $"Funcionário {funcionario.Cpf} já cadastrado");
                return false;
            }
            else
            {
                return base.Armazenar(funcionario, id);
            }
        }
    }
}
