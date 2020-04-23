package or.cuidabrasil.logic;

import io.vertx.core.json.Json;
import lombok.AllArgsConstructor;
import lombok.Data;

import java.util.HashMap;
import java.util.Map;

@Data
@AllArgsConstructor
public class BaseResult {

    public BaseResult() {
        this.statusCode = 599;
        this.body    = new HashMap<>();
        this.headers = new HashMap<>();
    }

    protected int    statusCode;
    protected Map<String, Object> body;
    protected Map<String,String> headers;

    public BaseResult putHeader(String key, String value) {
        this.headers.put(key,value);
        return this;
    }

    public BaseResult addData(String key, Object value) {
        this.body.put(key,value);
        return this;
    }

    public String getBodyAsString() {
        return Json.encode(this.body);
    }
}
