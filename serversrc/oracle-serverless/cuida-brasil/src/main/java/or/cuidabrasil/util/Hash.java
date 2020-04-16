package or.cuidabrasil.util;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class Hash {
	
	public static String md5(String str) { return computeHash(str, "MD5"); }
	
	public static String sha256(String str) { return computeHash(str, "SHA-256"); }
	
	public static String sha512(String str) { return computeHash(str, "SHA-512"); }
	
	private static String computeHash(String str, String algorithm) {
		try {
			MessageDigest mDigest = MessageDigest.getInstance(algorithm);
			mDigest.update(str.getBytes("UTF-8"));
			String result = "";
			for (Byte b : mDigest.digest()) 
				 result += String.format("%02x", b);
			return result;
		} catch (NoSuchAlgorithmException | UnsupportedEncodingException e) {
			throw new RuntimeException(e);
		}
	}


}
