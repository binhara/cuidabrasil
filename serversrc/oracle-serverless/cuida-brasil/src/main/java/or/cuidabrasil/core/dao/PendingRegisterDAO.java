package or.cuidabrasil.core.dao;

import or.cuidabrasil.core.models.PendingRegister;

public interface PendingRegisterDAO extends IGenericDAO<PendingRegister> {

    PendingRegister getByPhoneNumber(String phoneNumber);

    boolean insert(PendingRegister pendingRegister);

    boolean update(PendingRegister pendingRegister);

    boolean remove(PendingRegister pendingRegister);

}
