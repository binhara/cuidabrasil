package or.cuidabrasil.logic.validation;

public class ValidationLogic {

    private ValidationRequest request;
    private ValidationResult  result;

    protected ValidationLogic(ValidationRequest request) {
        this.request = request;
        this.result  = new ValidationResult();
    }

    public static ValidationResult execute(ValidationRequest request) throws Exception {
        return new ValidationLogic(request)._execute();
    }

    private ValidationResult _execute() {
        return null;
    }
}
