package or.cuidabrasil.logic.validation;

import or.cuidabrasil.core.dao.ContactDAO;
import or.cuidabrasil.core.dao.DAOFactory;
import or.cuidabrasil.core.dao.PendingRegisterDAO;
import or.cuidabrasil.core.models.Contact;
import or.cuidabrasil.core.models.PendingRegister;
import or.cuidabrasil.core.util.StatusCode;

public class ValidationLogic {

    private static final long PENDING_EXPIRATION_TIME = 1000*60*5;

    private ValidationRequest  request;
    private ValidationResult   result;
    private PendingRegisterDAO pRegisterDao;
    private PendingRegister    pendingRegister;
    private ContactDAO         contactDao;
    private Contact            contact;

    protected ValidationLogic(ValidationRequest request) throws Exception {
        this.request = request;
        this.result  = new ValidationResult();
        this.pRegisterDao = DAOFactory.getDao(PendingRegisterDAO.class);
        this.contactDao   = DAOFactory.getDao(ContactDAO.class);
    }

    public static ValidationResult execute(ValidationRequest request) throws Exception {
        return new ValidationLogic(request)._execute();
    }

    private ValidationResult _execute() {

        if(!this.validate())            return this.result;
        if(!this.findPendingRegister()) return this.result;
        loadContact();
        if(contact == null)
            this.handleNewContact();
        else
            this.handleExistentContact();

        return this.result;
    }

    private void handleNewContact() {
        //TODO
    }

    private void handleExistentContact() {
        //TODO
    }

    private void loadContact() {
        contact = contactDao.getByPhoneNumber(this.request.getContact().getPhone());
    }

    private boolean findPendingRegister() {

        pendingRegister = pRegisterDao.getByPhoneNumber(this.request.getContact().getPhone());
        if(pendingRegister == null) {
            System.out.println("No PendingRegister found for phone sent.");
            this.result.setStatusCode(StatusCode.BAD_REQUEST);
            this.result.addData("error", "invalid phone");
            return false;
        }

        if(!this.request.getCode().equals(pendingRegister.getCode())) {
            System.out.println("code doesnt match.");
            this.result.setStatusCode(StatusCode.BAD_REQUEST);
            this.result.addData("error", "invalid code.");
            return false;
        }

        if(this.pendingRegister.isExpired(PENDING_EXPIRATION_TIME)) {
            System.out.println("registration has expired.");
            this.result.setStatusCode(StatusCode.BAD_REQUEST);
            this.result.addData("error", "expired registration.");
            pRegisterDao.remove(pendingRegister);
            return false;
        }

        return true;
    }

    private boolean validate() {
        if(this.request.getCode() == null || this.request.getCode().isEmpty()) {
            this.result.setStatusCode(StatusCode.BAD_REQUEST);
            this.result.addData("error", "code is null or empty.");
            return false;
        }

        if(this.request.getContact() == null) {
            this.result.setStatusCode(StatusCode.BAD_REQUEST);
            this.result.addData("error", "contact is null.");
            return false;
        }

        return true;
    }
}
