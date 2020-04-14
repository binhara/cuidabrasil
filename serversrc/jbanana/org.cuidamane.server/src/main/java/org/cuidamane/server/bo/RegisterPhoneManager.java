package org.cuidamane.server.bo;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Random;
import java.util.Set;

/**
 * @author Charles Buss
 */
public class RegisterPhoneManager {

	private static final long PENDING_EXPIRATION_TIME = 1000*60*5;
	
	private static RegisterPhoneManager instance = null;
	
	public static RegisterPhoneManager getInstance() {
		if(instance == null) {
			instance = new RegisterPhoneManager();
		}
		return instance;
	}
	
	/**
	 * Generate a random code like 156-486
	 * @return String code ex: 156-456
	 */
	public String geenereateRandomCode() {
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
	
	
	//region <----- PENDING REGISTERS ----->
	
	private List<PendingRegister> pendingRegisters = new ArrayList<>();
	
	private PendingRegister getRegisterByCode(String code) {
		return pendingRegisters.stream()
			.filter(pr->pr.getCode().equals(code))
			.findFirst().orElse(null);
	}
	
	public PendingRegister addToPendingRegisters(String phoneNumber) {
		
		PendingRegister pr = pendingRegisters.stream()
				.filter(p->p.getPhone().equals(phoneNumber))
				.findFirst().orElse(null);
		
		if(pr != null) {
			pr.setCode(this.geenereateRandomCode())
				.setTimeStamp(System.currentTimeMillis());
			return pr;
		}
		
		pr = new PendingRegister()
				.setCode(this.geenereateRandomCode())
				.setPhone(phoneNumber)
				.setTimeStamp(System.currentTimeMillis());
		
		pendingRegisters.add(pr);
		return pr;
	}
	
	public boolean hasValidPendinRegister(String code, String phoneNumber) {
	
		PendingRegister pr = this.getRegisterByCode(code);
		if(pr == null)
			return false;
		
		if(pr.isExpired(PENDING_EXPIRATION_TIME)) {
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
	
	//endregion
	
	
}
