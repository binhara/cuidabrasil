package org.jbanana.core;

import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;

public interface Convetions {

	String PKG_KEY_CONFIG = "PACKAGE_CONFIG";
	String PKG_KEY_REST = "PACKAGE_REST";
	String PKG_KEY_BO = "PACKAGE_BO";
	String PKG_KEY_ROOT = "PACKAGE_ROOT";
	
	String PKG_VALUE_ROOT = "";
	String PKG_VALUE_BO = ".bo";
	String PKG_VALUE_REST = ".rest";
	String PKG_VALUE_CONFIG = ".config";
	
	String DELETE_TRIGGER[] = new String[]{"deleteAll", "deleteBy", "delete"};
	String GET_TRIGGER[] = new String[]{"restoreAll", "restoreBy", "restore"};
	String POST_TRIGGER[] = new String[]{"create"};
	String PUT_TRIGGER[] = new String[]{"replace"};
	String PATCH_TRIGGER[] = new String[]{"update"};
	
	enum Rest {post, get, put, patch, delete}
	enum Crud {create, restore, update, delete}
	
	@Retention(RetentionPolicy.RUNTIME)
	public @interface Singleton {}
}