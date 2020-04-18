package or.cuidabrasil.logic.registration;

import io.vertx.core.json.Json;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.experimental.Accessors;

import java.util.HashMap;
import java.util.Map;

@Data
@AllArgsConstructor
@Accessors(chain = true)
public class RegistrationResult {

    public RegistrationResult() {
        this.statusCode = 599;
        this.body    = new HashMap<>();
        this.headers = new HashMap<>();
    }

    private int    statusCode;
    private Map<String, Object> body;
    private Map<String,String> headers;

    public RegistrationResult putHeader(String key, String value) {
        this.headers.put(key,value);
        return this;
    }

    public RegistrationResult addData(String key, Object value) {
        this.body.put(key,value);
        return this;
    }

    public String getBodyAsString() {
        return Json.encode(this.body);
    }
}
