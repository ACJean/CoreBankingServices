using AccountOperations.Domain;
using AccountOperations.Domain.Entity;
using AccountOperations.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Application
{
    public class DefaultMovementsService : IMovementsService
    {

        private readonly ILogger<DefaultMovementsService> _logger;
        private readonly IAccountUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public DefaultMovementsService(ILogger<DefaultMovementsService> logger, IAccountUnitOfWork unitOfWork, IAccountService accountService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public void Add(Movements movement)
        {
            try
            {
                Account? account = _accountService.Get(movement.AccountNumber) ?? throw new InvalidOperationException("Account not found.");

                if ((account.Balance + movement.Amount) < 0)
                {
                    throw new InvalidAccountMovementException("Saldo no disponible");
                }

                _unitOfWork.BeginTransaction();

                movement.Date = DateTime.Now;
                movement.Type = account.Type;
                movement.Balance = account.Balance;
                int id = _unitOfWork.Movements.Add(movement);
                movement.Id = id;

                account.Balance += movement.Amount;
                _unitOfWork.Account.Update(account);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError(ex, "Error on create movement.");
                throw;
            }
        }

        public Movements? Get(int id)
        {
            try
            {
                return _unitOfWork.Movements.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on get movement.");
                throw;
            }
        }


    }
}
