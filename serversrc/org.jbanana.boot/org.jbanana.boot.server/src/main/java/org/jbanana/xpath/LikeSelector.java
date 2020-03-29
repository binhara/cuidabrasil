package org.jbanana.xpath;

public class LikeSelector implements Selector<String> {

	private final String like;
	private final Type type;
	
	private static enum Type { CONTAINS, STARTS, ENDS, EQUALS}
	
	public LikeSelector(String like) {
		this.type = findType(like);
		this.like = like.replaceAll("\\*", "");
	}

	public static Type findType(String like) {
		if(like.charAt(like.length()-1)=='*'){
			if(like.charAt(0)=='*')
				return Type.CONTAINS;
			return Type.STARTS;	
		}
		
		if(like.charAt(0)=='*')
			return Type.ENDS;

		return Type.EQUALS;
	}

	@Override
	public boolean match(String candidate) {
		if(type == Type.EQUALS)
			if(candidate.equals(like))
				return true;
		
		if(type == Type.CONTAINS)
			if(candidate.contains(like))
				return true;
		
		if(type == Type.ENDS)
			if(candidate.endsWith(like))
				return true;
		
		if(type == Type.STARTS)
			if(candidate.startsWith(like))
				return true;

		return false;
	}
}
