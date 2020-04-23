package or.cuidabrasil.core.dao;

import or.cuidabrasil.core.models.Contact;

public interface ContactDAO extends IGenericDAO {

    Contact getByPhoneNumber(String phoneNumber);

    Contact insert(Contact contact);

    Contact update(Contact contact);

    boolean remove(Contact contact);


}
