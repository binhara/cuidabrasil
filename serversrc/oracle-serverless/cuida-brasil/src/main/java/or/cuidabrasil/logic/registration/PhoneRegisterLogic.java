package or.cuidabrasil.logic.registration;

import or.cuidabrasil.core.dao.DAOFactory;
import or.cuidabrasil.core.dao.PendingRegisterDAO;
import or.cuidabrasil.core.models.PendingRegister;
import or.cuidabrasil.core.util.StatusCode;

import java.util.HashSet;
import java.util.Random;
import java.util.Set;

/**
 * Cria uma solicitação de registro de telefone.
 * essa classe usa a interface {@link PendingRegisterDAO} para acessar o banco
 * e fazer as operações necessarias.
 */
public class PhoneRegisterLogic {

    private RegistrationRequest request;
    private RegistrationResult result;
    private PendingRegisterDAO  pRegisterDao;
    private PendingRegister     pendingRegister;

    protected PhoneRegisterLogic(RegistrationRequest request) throws Exception {
        this.request = request;
        this.pendingRegister = null;
        this.result  = new RegistrationResult();
        this.pRegisterDao = DAOFactory.getDao(PendingRegisterDAO.class);
    }

    public static RegistrationResult execute(RegistrationRequest request) throws Exception {
        return new PhoneRegisterLogic(request)._execute();
    }

    private RegistrationResult _execute() {

        if(!this.validate())                   return this.result;
        if(!this.removeExistentRegistration()) return this.result;
        if(!this.createPendingRegister())      return this.result;

        // retorna temporariamnet o temp_code para testes internos.
        return new RegistrationResult().setStatusCode(StatusCode.SUCCESS)
                .addData("temp_code", this.pendingRegister.getCode());
    }

    private boolean createPendingRegister() {
        pendingRegister = new PendingRegister()
                .setCode(this.geenereateRandomCode())
                .setTimeStamp(System.currentTimeMillis())
                .setPhone(this.request.getPhoneNumber());

        if(pRegisterDao.insert(pendingRegister))
            return true;

        this.result.setStatusCode(StatusCode.INTERNAL_SERVER_ERROR);
        return false;
    }

    /**
     * Remove uma solicitacao de regsitro existente
     * @return
     */
    private boolean removeExistentRegistration() {
        PendingRegister openRegister = pRegisterDao.getByPhoneNumber(request.getPhoneNumber());
        if(openRegister == null)
            return true;
        if(pRegisterDao.remove(openRegister))
           return true;

        this.result.setStatusCode(StatusCode.INTERNAL_SERVER_ERROR);
        return false;
    }

    private boolean validate() {
        if(this.request == null) {
            this.result.setStatusCode(StatusCode.BAD_REQUEST);
            return false;
        }

        if(this.request.getPhoneNumber() == null || this.request.getPhoneNumber().isEmpty()){
            this.result.setStatusCode(StatusCode.BAD_REQUEST);
            return false;
        }

        return true;
    }


    /**
     * Generate a random code like 156-486
     * @return String code ex: 156-456
     */
    private String geenereateRandomCode() {
        Random rnd = new Random(System.currentTimeMillis());

        StringBuilder result = new StringBuilder();
        for(int i = 0; i < 3; i++) {
            Set<Integer> part = new HashSet<>();
            for (int j = 1; j < 2; j++)
            {
                Set<Integer> set = new HashSet<>();
                while (set.size() < 2)
                {
                    int random = rnd.nextInt(9);
                    if (part.add(random)) {
                        set.add(random);
                    }
                }
            }
            part.forEach(result::append);
        }
        return result.insert(3, "-").toString();
    }

}
