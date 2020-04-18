package or.cuidabrasil.core.models;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.experimental.Accessors;

import java.io.Serializable;

/**
 * @author Charles Buss
 */
@Data
@NoArgsConstructor
@AllArgsConstructor
@Accessors(chain = true)
public class PendingRegister implements Serializable {

    private String code;
    private String phone;
    private long   timeStamp;

    public boolean isExpired(long validPeriod) {
        return System.currentTimeMillis()-timeStamp > validPeriod;
    }

}