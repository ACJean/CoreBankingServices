using AccountOperations.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Application
{
    public class MovementsService
    {

        private readonly ILogger<MovementsService> _logger;
        private readonly IAccountUnitOfWork _unitOfWork;
        private readonly AccountService _accountService;

        public MovementsService(ILogger<MovementsService> logger, IAccountUnitOfWork unitOfWork, AccountService accountService)
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
                    throw new InvalidOperationException("Saldo no disponible");
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
