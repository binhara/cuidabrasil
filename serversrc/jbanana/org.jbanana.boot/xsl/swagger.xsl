<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="text" encoding="UTF-8" media-type="text/plain"/>

<xsl:template match="/">swagger: '2.0'
info:
  version: <xsl:value-of select="object-array/list/org.jbanana.Property[key='version']/value" />
  title: <xsl:value-of select="object-array/list/org.jbanana.Property[key='title']/value" />
host: <xsl:value-of select="object-array/list/org.jbanana.Property[key='host']/value" />
paths:
<xsl:for-each-group select="//org.jbanana.rest.RestMap" group-by="path">
<xsl:apply-templates select="swaggerPath"/><xsl:text>
</xsl:text>
</xsl:for-each-group>
definitions:
<xsl:apply-templates select="object-array/list/org.jbanana.rest.ObjectDefinition"/>
</xsl:template>

<xsl:template match="swaggerPath">
<xsl:text>
  </xsl:text><xsl:value-of select="." />:
<xsl:call-template name="rest">
<xsl:with-param name="param" select="."/>
</xsl:call-template>
</xsl:template>

<xsl:template name="rest">
<xsl:param name="param"/>
<xsl:for-each select="/.//org.jbanana.rest.RestMap[swaggerPath=$param]/swaggerPath">
<xsl:text>
    '</xsl:text><xsl:value-of select="../@rest" />'<xsl:text>:
      tags:
        - </xsl:text><xsl:value-of select="../@entityName" /><xsl:text>
      responses:
        '200':
          description: successful operation
          schema:</xsl:text>

<xsl:for-each select="../targetClass[@returnIsArray='false' and @returnIsBoolean='true']">
            type: boolean</xsl:for-each>   
          
<xsl:for-each select="../targetClass[@returnIsArray='false' and @returnIsBoolean='false']">
            $ref: '#/definitions/<xsl:value-of select="../@entityName" />'</xsl:for-each>   
          
<xsl:for-each select="../targetClass[@returnIsArray='true']">
            type: array
            items:
              $ref: '#/definitions/<xsl:value-of select="../@entityName" />'</xsl:for-each>          
<xsl:apply-templates select="../request"/></xsl:for-each>	
</xsl:template>

<xsl:template match="request">
      parameters:<xsl:apply-templates select="key"/><xsl:apply-templates select="fields"/> 
</xsl:template>

<xsl:template match="key">
      - in: path
        type: string
        name: <xsl:value-of select="@name" />
        required: true
</xsl:template>

<xsl:template match="fields">
      - in: body
        name: body
        required: true
        schema:
          type: object
          properties:
<xsl:apply-templates select="field"/> 
</xsl:template>

<xsl:template match="field">
<xsl:text>            </xsl:text><xsl:value-of select="@name"/>:
<xsl:text>              type: </xsl:text><xsl:value-of select="@type"/><xsl:text>
</xsl:text>
<xsl:apply-templates select="enum"/>
</xsl:template>

<xsl:template match="enum">
<xsl:text>              </xsl:text>enum:<xsl:for-each select="item">
<xsl:text>
              - </xsl:text><xsl:value-of select="." />
</xsl:for-each>
</xsl:template>

<xsl:template match="org.jbanana.rest.ObjectDefinition">
<xsl:text>
  </xsl:text><xsl:value-of select="entityName" />:
    type: object
    properties:
<xsl:apply-templates select="properties/java.lang.reflect.Field[@isStatic='false']"/>
</xsl:template>

<xsl:template match="java.lang.reflect.Field">
<xsl:text>      </xsl:text><xsl:value-of select="@name"/>:
<xsl:text>        type: </xsl:text><xsl:value-of select="@type"/><xsl:text>
</xsl:text>
</xsl:template>

</xsl:stylesheet>