package or.cuidabrasil.core.dao;

import or.cuidabrasil.core.models.PendingRegister;

public interface PendingRegisterDAO extends IGenericDAO {

    PendingRegister getByPhoneNumber(String phoneNumber);

    PendingRegister getByCode(String code);

    boolean insert(PendingRegister pendingRegister);

    boolean update(PendingRegister pendingRegister);

    boolean remove(PendingRegister pendingRegister);

}
