package org.cuidamane.server.bo;

import java.util.ArrayList;
import java.util.List;

import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.experimental.Accessors;

public class RegisterPhoneManager {

	private static final long EXPIRATION_TIME = 1000*60*5;
	
	private static RegisterPhoneManager instance = null;
	
	public static RegisterPhoneManager getInstance() {
		if(instance == null) {
			instance = new RegisterPhoneManager();
		}
		return instance;
	}
	
	private List<PendingRegister> pendingRegisters = new ArrayList<>();
	
	private PendingRegister getRegisterByCode(String code) {
		return pendingRegisters.stream()
			.filter(pr->pr.getCode().equals(code))
			.findFirst().orElse(null);
	}
	
	public void addToPendingRegisters(String code, String phoneNumber) {
		
		PendingRegister pr = pendingRegisters.stream()
				.filter(p->p.getPhone().equals(phoneNumber))
				.findFirst().orElse(null);
		
		if(pr != null) {
			pr.setCode(code)
				.setTimeStamp(System.currentTimeMillis());
			return;
		}
		
		pendingRegisters.add(new PendingRegister()
				.setCode(code)
				.setPhone(phoneNumber)
				.setTimeStamp(System.currentTimeMillis()));
		return;
	}
	
	public boolean hasValidPendinRegister(String code, String phoneNumber) {
	
		PendingRegister pr = this.getRegisterByCode(code);
		if(pr == null)
			return false;
		
		if(pr.isExpired(EXPIRATION_TIME)) {
			pendingRegisters.remove(pr);
			return false;
		}
		
		if(!pr.getPhone().equals(phoneNumber))
			return false;
		
		return true;
	}
	
	public void removePendingRegister(String code, String phone) {
		PendingRegister pr = pendingRegisters.stream()
				.filter(p->p.getCode().equals(code) && p.getPhone().equals(phone))
				.findFirst().orElse(null);
		if(pr != null)
			pendingRegisters.remove(pr);
	}
	
	@Data
	@NoArgsConstructor
	@Accessors(chain = true)
	class PendingRegister {
		
		private String code;
		private String phone;
		private long   timeStamp;
		
		public boolean isExpired(long validPeriod) {
			return System.currentTimeMillis()-timeStamp > validPeriod;
		}
	}
	
	
}
