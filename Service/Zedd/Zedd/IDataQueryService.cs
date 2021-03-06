﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using Zedd.Dto;

namespace Zedd
{
  [ServiceContract]
  public interface IDataQueryService
  {
    [OperationContract]
    DeansOfficeInfo GetDeansOfficeInfo(int id, Guid ticket);

    [OperationContract]
    LibraryInfo GetLibrary(int id, Guid ticket);

    [OperationContract]
    MessageInfo GetMessage(int id, Guid ticket);

    [OperationContract]
    EventInfo GetEvent(int id, Guid ticket);

    [OperationContract]
    UnitInfo GetUnit(int id, Guid ticket);

    [OperationContract]
    UserInfo GetUser(int id, Guid ticket);

    [OperationContract]
    Department GetDepartment(int id, Guid ticket);

    [OperationContract]
    IList<DeansOfficeInfo> GetAllDeansOffices(Guid ticket);

    [OperationContract]
    IList<LibraryInfo> GetLibraries(Guid ticket);

    [OperationContract]
    IList<MessageInfo> GetMessages(Guid ticket, DateTime? startDate);

    [OperationContract]
    IList<EventInfo> GetEvents(Guid ticket, DateTime? startDate, DateTime? endDate);

    [OperationContract]
    IList<UnitInfo> GetUnits(Guid ticket);

    [OperationContract]
    IList<UserInfo> GetUsers(Guid ticket);
  }
}