package or.cuidabrasil.logic.validation;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.experimental.Accessors;
import or.cuidabrasil.core.models.Contact;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Accessors(chain = true)
public class ValidationRequest {

    private String code;
    private Contact contact;

}
