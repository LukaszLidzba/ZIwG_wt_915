﻿using System;
using System.ServiceModel;
using Zedd.Commands;
using Zedd.Dto;
using Zedd.Exceptions;
using Zedd.Queries;

namespace Zedd
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LoginService" in code, svc and config file together.
  // NOTE: In order to launch WCF Test Client for testing this service, please select LoginService.svc or LoginService.svc.cs at the Solution Explorer and start debugging.
  public class LoginService : ILoginService
  {
    private readonly ILoginHandler _loginHandler;
    private readonly ILoginQueryDao _loginQueryDao;
    readonly ICommands _commands;

    public LoginService()
      : this(null, null, null)
    {
    }

    public LoginService(ILoginHandler loginHandler, ILoginQueryDao loginQueryDao, ICommands commands)
    {
      _loginHandler = loginHandler ?? new LoginHandler();
      _loginQueryDao = loginQueryDao ?? new LoginQueryDao();
      _commands = commands ?? new Commands.Commands();
    }

    public string Login(string loginName, string password)
    {
      Guid token;

      try
      {
        token = _loginHandler.HandleLogin(loginName, password);
      }
      catch (WrongPasswordException e)
      {
        throw new FaultException(new FaultReason("Wrong login or password") + e.Message);
      }
      catch (Exception e)
      {
        throw new FaultException(new FaultReason("Error during login") + e.Message + e.InnerException + e.StackTrace + e.Data);
      }

      return token.ToString();
    }

    public bool TryLogin(string loginName, string password)
    {
      return _loginHandler.HandleTryLogin(loginName, password);
    }

    public void ChangePassword(ChangePasswordRequest request)
    {
      try
      {
        _loginHandler.HandleProlongSession(request.TicketId);

        var user = _loginQueryDao.GetUser(request.TicketId);

        _commands.ChangePassword(user.Id, request.NewPassword, request.OldPassword);
      }
      catch (Exception e)
      {
        throw new FaultException(new FaultReason("Error during password changing") + e.Message + e.InnerException + e.StackTrace + e.Data);
      }
    }

    public void ProlongSession(Guid ticketId)
    {
      try
      {
        _loginHandler.HandleProlongSession(ticketId);
      }
      catch (Exception e)
      {
        throw new FaultException(new FaultReason("Error during prolong session") + e.Message + e.InnerException + e.StackTrace + e.Data);
      }
    }

    public UserInfo GetUser(Guid ticketId)
    {
      UserInfo result;

      try
      {
        _loginHandler.HandleProlongSession(ticketId);

        result = _loginQueryDao.GetUser(ticketId);
      }
      catch (Exception e)
      {
        throw new FaultException(new FaultReason("Error during get User") + e.Message + e.InnerException + e.StackTrace + e.Data);
      }

      return result;
    }
  }
}