package org.cuidamane.server.bo;

import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.experimental.Accessors;

/**
 * @author Charles Buss
 */
@Data
@NoArgsConstructor
@Accessors(chain = true)
public class PendingRegister {
	
	private String code;
	private String phone;
	private long   timeStamp;

	public boolean isExpired(long validPeriod) {
		return System.currentTimeMillis()-timeStamp > validPeriod;
	}
	
}