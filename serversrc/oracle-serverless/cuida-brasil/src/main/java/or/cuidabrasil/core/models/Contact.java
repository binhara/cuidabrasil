package or.cuidabrasil.core.models;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.experimental.Accessors;

import java.io.Serializable;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Accessors(chain = true)
public class Contact implements Serializable {

    private String id;
    private String name;
    private String phone;
    private String pushToken;
    private int    age;

}
