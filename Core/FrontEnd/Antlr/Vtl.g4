grammar Vtl;
import VtlTokens;

start: 
(statement? (ML_COMMENT)* (SL_COMMENT)* EOL)* statement? EOF
|ML_COMMENT
|SL_COMMENT
;

statement: 
(datasetID ASSIGN)? (dataset|scalar)
|datasetID PUT_SYMBOL dataset
|defExpr
;

dataset:
closedDataset
|membershipDataset
|openedDataset
;

openedDataset:
ifThenElseDataset
|opSymbol=NOT dataset
|unopenedDataset opSymbol=('+'|'-') (openedDataset|closedDataset|membershipDataset|scalar)
|unopenedDataset opSymbol=('*'|'/') (closedDataset|membershipDataset|constant|scalar|openedDataset)
|unopenedDataset opSymbol=(AND|OR|XOR|CONCAT) (openedDataset|closedDataset|membershipDataset|scalar)
|unopenedDataset opSymbol=('>'|'<'|'<='|'>='|'='|'<>') (closedDataset|membershipDataset|constant|scalar|openedDataset)
|unopenedDataset opSymbol=(IN|NOT_IN) (list|valueDomainName)
|openedDatasetLeft=openedDataset opSymbol=('+'|'-') (openedDataset|closedDataset|membershipDataset|scalar)
|openedDatasetLeft=openedDataset opSymbol=('*'|'/') (closedDataset|membershipDataset|constant|scalar|openedDataset)
|openedDatasetLeft=openedDataset opSymbol=(AND|OR|XOR|CONCAT) (openedDataset|closedDataset|membershipDataset|scalar)
|openedDatasetLeft=openedDataset opSymbol=('>'|'<'|'<='|'>='|'='|'<>') (closedDataset|membershipDataset|constant|scalar|openedDataset)
|openedDatasetLeft=openedDataset opSymbol=(IN|NOT_IN) (list|valueDomainName)
|(constant|scalar) opSymbol=('+'|'-') (openedDataset|closedDataset|membershipDataset)
|(constant|scalar) opSymbol=('*'|'/') (closedDataset|membershipDataset|openedDataset)
|(constant|scalar) opSymbol=(AND|OR|XOR|CONCAT) (openedDataset|closedDataset|membershipDataset)
|(constant|scalar) opSymbol=('>'|'<'|'<='|'>='|'='|'<>') (closedDataset|membershipDataset|openedDataset)
;

closedDataset:
datasetID
|datasetComplex
|closedDataset '[' datasetClause ']'
|'(' datasetID ')'
|'(' closedDataset ')'
|'(' membershipDataset ')'
|'(' openedDataset ')'
|opSymbol=('+'|'-') dataset
|opSymbol=ROUND '(' dataset (',' optionalExpr)? ')'
|opSymbol=CEIL '(' dataset ')'                           
|opSymbol=FLOOR '(' dataset ')'                    
|opSymbol=ABS '(' dataset ')'               
|opSymbol=EXP '(' dataset ')'          
|opSymbol=LN '(' dataset ')'           
|opSymbol=LOG '(' dataset ',' scalar ')'						
|opSymbol=TRUNC '(' dataset (',' optionalExpr)? ')'	
|opSymbol=POWER '(' dataset ',' scalar ')'					
|opSymbol=SQRT '(' dataset ')'		
|opSymbol=LEN '(' dataset ')'							
|opSymbol=BETWEEN '(' dataset ',' scalar ',' scalar ')'			
|opSymbol=TRIM '(' dataset ')'
|opSymbol=LTRIM '(' dataset ')'    
|opSymbol=RTRIM '(' dataset ')'				    
|opSymbol=UCASE '(' dataset ')'				    
|opSymbol=LCASE '(' dataset ')'			    
|opSymbol=SUBSTR '(' dataset (',' optionalExpr)? (',' optionalExpr)? ')'
|opSymbol=INSTR '(' dataset ',' scalar (',' optionalExpr)? (',' optionalExpr)? ')'
|opSymbol=REPLACE '(' dataset ',' scalar (',' optionalExpr)? ')'			
|opSymbol=CHARSET_MATCH '(' dataset ',' scalar ')'
|opSymbol=ISNULL '(' dataset ')'		
|opSymbol=NVL '(' dataset ',' (dataset | scalar) ')'
|opSymbol=MOD '(' dataset ',' (dataset | scalar) ')'			
|opSymbol=EXISTS_IN '(' dataset ',' dataset (',' retainType)? ')'
|opSymbol=FLOW_TO_STOCK '(' dataset ')'
|opSymbol=STOCK_TO_FLOW '(' dataset ')'             
|opSymbol=PERIOD_INDICATOR '(' dataset ')'	
|opSymbol=TIMESHIFT '(' dataset ',' scalar ')'
|opSymbol=FILL_TIME_SERIES '(' dataset (',' (limitsMethod))? ')'
|opSymbol=TIME_AGG '(' scalar ',' dataset ')'
;

membershipDataset:
closedDataset MEMBERSHIP componentID
;

datasetComplex:
aggrInvocation
|analyticInvocation
|setExpr
|joinExpr
|checkDatapoint
;

ifThenElseDataset:
IF ifDataset=dataset THEN (thenDataset=dataset|thenScalar=scalar) ELSE (elseDataset=dataset|elseScalar=scalar)
|IF (ifDataset=dataset|ifScalar=scalar) THEN thenDataset=dataset ELSE (elseDataset=dataset|elseScalar=scalar)
|IF (ifDataset=dataset|ifScalar=scalar) THEN (thenDataset=dataset|thenScalar=scalar) ELSE elseDataset=dataset
;

unopenedDataset:
closedDataset
|membershipDataset
;

component:
(closedDataset MEMBERSHIP)? componentID
;

scalar:
(constant|component)
|'(' scalar ')'
|ifThenElseScalar
|opSymbol=('+'|'-') component
|opSymbol=NOT scalar
|scalar opSymbol=('*'|'/') scalar
|scalar opSymbol=('+'|'-') scalar
|scalar opSymbol=('>'|'<'|'<='|'>='|'='|'<>') scalar
|scalar opSymbol=(AND|OR|XOR|CONCAT) scalar
|scalar opSymbol=(IN|NOT_IN) (list|valueDomainName)
|opSymbol=ROUND '(' scalar (',' optionalExpr)? ')'
|opSymbol=CEIL '(' scalar ')'                           
|opSymbol=FLOOR '(' scalar ')'                    
|opSymbol=ABS '(' scalar ')'               
|opSymbol=EXP '(' scalar ')'          
|opSymbol=LN '(' scalar ')'           
|opSymbol=LOG '(' scalar ',' scalar ')'						
|opSymbol=TRUNC '(' scalar (',' optionalExpr)? ')'	
|opSymbol=POWER '(' scalar ',' scalar ')'					
|opSymbol=SQRT '(' scalar ')'		
|opSymbol=LEN '(' scalar ')'							
|opSymbol=BETWEEN '(' scalar ',' scalar ',' scalar ')'			
|opSymbol=TRIM '(' scalar ')'
|opSymbol=LTRIM '(' scalar ')'    
|opSymbol=RTRIM '(' scalar ')'				    
|opSymbol=UCASE '(' scalar ')'				    
|opSymbol=LCASE '(' scalar ')'			    
|opSymbol=SUBSTR '(' scalar (',' optionalExpr)? (',' optionalExpr)? ')'
|opSymbol=INSTR '(' scalar ',' scalar (',' optionalExpr)? (',' optionalExpr)? ')'
|opSymbol=REPLACE '(' scalar ',' scalar (',' optionalExpr)? ')'			
|opSymbol=CHARSET_MATCH '(' scalar ',' scalar ')'
|opSymbol=ISNULL '(' scalar ')'		
|opSymbol=NVL '(' scalar ',' scalar ')'
|opSymbol=MOD '(' scalar ',' scalar ')'			
|opSymbol=PERIOD_INDICATOR '(' scalar? ')'
|opSymbol=TIME_AGG '(' scalar ',' scalar ')'
|opSymbol=CURRENT_DATE
;

ifThenElseScalar:
IF scalar THEN scalar ELSE scalar
;

optionalExpr: 
scalar
|OPTIONAL
;

setExpr:
opSymbol=UNION '(' dataset (',' dataset)* ')'		
|opSymbol=SYMDIFF '(' dataset ',' dataset ')' 
|opSymbol=SETDIFF '(' dataset ',' dataset ')'
|opSymbol=INTERSECT '(' dataset (',' dataset)* ')'
;

datasetClause:
aggrClause
|analyticClause
|filterClause
|renameClause
|calcClause
|keepClause
|dropClause
|pivotClause
|unpivotClause
|subspaceClause
;

aggrClause : 
AGGREGATE aggrExpr (',' aggrExpr)* groupingClause? havingClause?
;

aggrExpr:
componentRole? componentID ':=' aggrFunction
;

filterClause:
FILTER scalar 
;

renameClause:
RENAME renameExpr (',' renameExpr)* 
;

renameExpr:
component TO componentID
;

calcClause:
CALC calcExpr (',' calcExpr)*
;

calcExpr:
componentRole? componentID ':=' (scalar|analyticFunction)
;

keepClause:
KEEP component (',' component)*
;

dropClause:
DROP component (',' component)*
;

pivotClause:
PIVOT componentID ',' componentID
;

unpivotClause
:
UNPIVOT componentID ',' componentID
;

subspaceClause:
SUBSPACE subspaceExpr (',' subspaceExpr)*
;

subspaceExpr:
component '=' constant
;

joinExpr:
joinKeyword '(' joinClause joinBody? ')'
;

joinClause:
joinAliasesClause joinUsingClause?
;

joinBody:
joinFilterClause? (joinCalcClause|joinApplyClause|(joinAggrClause groupingClause havingClause?))? (joinKeepClause|joinDropClause)? joinRenameClause?
;

joinAliasesClause:
joinAliasExpr (',' joinAliasExpr)*
;

joinAliasExpr:
dataset (AS varID)?
;

joinUsingClause:
USING componentID (',' componentID)*
;

joinCalcClause:
calcClause
;

joinAggrClause:
aggrClause
;
  
joinKeepClause:
keepClause
;

joinDropClause:
dropClause
;

joinFilterClause:
filterClause
;

joinRenameClause:
renameClause
;
  
joinApplyClause:
APPLY scalar
; 

aggrInvocation:
opSymbol=aggrFunctionName '(' dataset groupingClause havingClause? ')'
;

aggrFunction:
opSymbol=SUM '(' component ')'
|opSymbol=AVG '(' component ')'
|opSymbol=COUNT '(' component? ')'
|opSymbol=MEDIAN '(' component ')'
|opSymbol=MIN '(' component ')'
|opSymbol=MAX '(' component ')'
|opSymbol=RANK '(' component ')'
|opSymbol=STDDEV_POP '(' component ')'
|opSymbol=STDDEV_SAMP '(' component ')'
|opSymbol=VAR_POP '(' component ')'
|opSymbol=VAR_SAMP '(' component ')'
;

aggrFunctionName:
SUM 
|AVG 
|COUNT 
|MEDIAN 
|MIN 
|MAX
|STDDEV_POP 
|STDDEV_SAMP
|VAR_POP 
|VAR_SAMP
;

groupingClause:
groupKeyword component (',' component)*
;
   
havingClause:
HAVING havingExpr
|HAVING '(' havingExpr ')'
;

havingExpr:
leftScalar=scalar opSymbol=('>'|'<'|'<='|'>='|'='|'<>') aggrFunction
|leftAggrFunction=aggrFunction opSymbol=('>'|'<'|'<='|'>='|'='|'<>') scalar
|havingExpr opSymbol=(AND|OR|XOR) havingExpr
;

analyticInvocation:
aggrOpSymbol=aggrFunctionName '(' dataset OVER '(' analyticClause ')' ')'
|opSymbol=(FIRST_VALUE|LAST_VALUE) '(' dataset OVER '(' analyticClause ')' ')'
|opSymbol=RATIO_TO_REPORT '(' dataset OVER '(' partitionClause ')' ')'
|opSymbol=(LAG|LEAD) '(' dataset ',' scalar (',' scalar)? OVER '(' partitionClause? orderClause ')' ')'
;

analyticFunction:
aggrFunctionName '(' component OVER '(' analyticClause ')' ')'
|opSymbol=(FIRST_VALUE|LAST_VALUE) '(' component OVER '(' analyticClause ')' ')'
|opSymbol=RANK '(' OVER '(' partitionClause? orderClause ')' ')'
|opSymbol=RATIO_TO_REPORT '(' component OVER '(' partitionClause ')' ')'
|opSymbol=(LAG|LEAD) '(' component ',' scalar (',' scalar)? OVER '(' partitionClause? orderClause ')' ')'
;

analyticClause:
partitionClause orderClause? windowingClause?
|partitionClause? orderClause windowingClause?
|partitionClause? orderClause? windowingClause
;

partitionClause:
PARTITION BY component (',' component)*
;
  
orderClause:
ORDER BY orderExpr (',' orderExpr)* 
;

orderExpr:
component (ASC|DESC)?
;
  
windowingClause:
((DATA POINTS)|RANGE) BETWEEN firstWindowLimit AND secondWindowLimit
;
  
firstWindowLimit:
INTEGER_CONSTANT PRECEDING
|CURRENT DATA POINT
|UNBOUNDED PRECEDING
;   

secondWindowLimit:
INTEGER_CONSTANT FOLLOWING
|CURRENT DATA POINT
|UNBOUNDED FOLLOWING
;

analyticFunctionName:
FIRST_VALUE 
|LAST_VALUE 
|LAG 
|RANK 
|RATIO_TO_REPORT 
|LEAD 
;

list:
'{' scalar (',' scalar)* '}'
;

varID:
IDENTIFIER
;

datasetID: 
IDENTIFIER
|IDENTIFIER '\\' IDENTIFIER
;
  
componentID:
IDENTIFIER
;

joinKeyword:
INNER_JOIN
|LEFT_JOIN
|FULL_JOIN
|CROSS_JOIN
;

groupKeyword:
GROUP BY
|GROUP EXCEPT
|GROUP ALL
;

constant:
FLOAT_CONSTANT
|INTEGER_CONSTANT
|BOOLEAN_CONSTANT
|STRING_CONSTANT
|TIME_CONSTANT
|NULL_CONSTANT
;

componentRole:
MEASURE
|DIMENSION
|ATTRIBUTE
|VIRAL ATTRIBUTE
;

valueDomainName:
IDENTIFIER
;

retainType:
BOOLEAN_CONSTANT
|ALL
;

limitsMethod:
ALL
|SINGLE
;

checkDatapoint:
CHECK_DATAPOINT '(' dataset ',' rulesetID (COMPONENTS componentID (',' componentID)*)? output=(INVALID|ALL|ALL_MEASURES)? ')'
;

// --- RULESETS:

defExpr:
defDatapoint
//|defHierarchical
//|defOperator
;

defDatapoint:
DEFINE DATAPOINT RULESET rulesetID '(' rulesetSignature ')' IS ruleClauseDatapoint END DATAPOINT RULESET
;

rulesetSignature:
signatureType=(VALUE_DOMAIN|VARIABLE) varSignature (',' varSignature)*
;

ruleClauseDatapoint:
ruleItemDatapoint (';' ruleItemDatapoint)*
;

ruleItemDatapoint:
(ruleID ':')? ( WHEN scalar THEN )? scalar errorCode? errorLevel?
;

varSignature:
varID (AS IDENTIFIER)?
;  

errorCode :
ERRORCODE constant
;
  
errorLevel:
ERRORLEVEL constant
;

rulesetID:
IDENTIFIER
;

ruleID:
IDENTIFIER
;

// ------------
 
/*

expr: 
dataset
;

exprComplex:
;

timeExpr
 :timeSeriesExpr
 |periodExpr (opComp=('>'|'<'|'<='|'>='|'='|'<>') expr)?
 |timeShiftExpr
 |timeAggExpr
 |CURRENT_DATE
 ;      
    
defHierarchical
  :
  defineHierarchicalRuleset rulesetID '(' hierRuleSignature ')' IS ruleClauseHierarchical endHierarchicalRuleset
  ;
ruleClauseHierarchical
  :
  ruleItemHierarchical (';' ruleItemHierarchical)*
  ;
ruleItemHierarchical
  :
  (IDENTIFIER ':')? codeItemRelation (erCode)? (erLevel)?
  ;
  
 hierRuleSignature
  :
  (VALUE_DOMAIN|VARIABLE) valueDomainSignature? RULE IDENTIFIER
  ; 
  
 valueDomainSignature
  :
  CONDITION IDENTIFIER (AS IDENTIFIER)? (',' IDENTIFIER (AS IDENTIFIER)?)*
  ; 
  
codeItemRelation
  :
  ( WHEN expr THEN )? codeItemRef codeItemRelationClause (codeItemRelationClause)*
  ;

codeItemRelationClause
  :
  (opAdd=('+'|'-'))? IDENTIFIER ('[' expr ']')?
  ;
  
codeItemRef
  :
  IDENTIFIER (opComp=('='|'>'|'<'|'>='|'<='))?
  ;

  
defOperator
  :
  DEFINE OPERATOR operatorID '(' (parameterItem (',' parameterItem)*)? ')' (RETURNS dataType)? IS expr END OPERATOR
  ;  
 
parameterItem
  :
  varID dataType (DEFAULT constant)?
  ;
    
callFunction
  :
  operatorID '(' ((constant|'_') (',' (constant|'_'))*)? ')'
  ;   

exprAtom
  :
  ROUND '(' expr (',' optionalExpr)? ')'							# roundAtom
  | CEIL '(' expr ')'												# ceilAtom
  | FLOOR '(' expr ')'												# floorAtom
  | ABS '(' expr ')'												# minAtom
  | EXP '(' expr ')'												# expAtom
  | LN '(' expr ')'													# lnAtom
  | LOG '(' expr ',' expr ')'										# logAtom
  | TRUNC '(' expr (',' optionalExpr)? ')'							# lnAtom
  | POWER '(' expr ',' expr ')'										# powerAtom
  | SQRT '(' expr ')'												# sqrtAtom
  | LEN '(' expr ')'												# lenAtom
  | BETWEEN '(' expr ',' expr ',' expr ')'							# betweenAtom
  | TRIM '(' expr ')'												# trimAtom
  | LTRIM '(' expr ')'												# ltrimAtom
  | RTRIM '(' expr ')'												# rtrimAtom
  | UCASE '(' expr ')'												# ucaseAtom
  | LCASE '(' expr ')'												# lcaseAtom
  | SUBSTR '(' expr (',' optionalExpr)? (',' optionalExpr)? ')'		# substrAtom
  | INSTR '(' expr ',' expr ( ',' optionalExpr)? (',' optionalExpr)? ')'	# instrAtom
  | REPLACE '(' expr ',' expr ( ',' optionalExpr)? ')'				# replaceAtom
  | CHARSET_MATCH '(' expr ','  expr ')'							# charsetMatchAtom
  | ISNULL '(' expr ')'												# isNullAtom
  | NVL '(' expr ',' expr ')'										# nvlAtom
  | MOD '(' expr ',' expr ')'										# modAtom
  | ref																# refAtom
  | evalExpr														# evalExprAtom
  | castExpr														# castExprAtom
  | hierarchyExpr													# hierarchyExprAtom
  | FLOW_TO_STOCK '(' expr ')'										# flowToStockAtom
  | STOCK_TO_FLOW '(' expr ')'										# stockToFlowAtom
  | validationDatapoint												#validateDPruleset
  | validationHierarchical 											#validateHRruleset
  | validationExpr													#validationSimple
  ;

ref: '(' expr ')'													# parenthesisExprRef
  | varID															# varIdRef
  | constant														# constantRef
  ; 		
  
identifierList
  :
  '[' IDENTIFIER (',' IDENTIFIER)* ']'
  ;			   

evalExpr
  :
  EVAL '(' routineName '(' (componentID|constant)? (',' (componentID|constant))* ')' (LANGUAGE STRING_CONSTANT)? (RETURNS outputParameterType)? ')'
  ;
  
castExpr
  :  
  CAST '(' expr ',' (basicScalarType|valueDomainName) (',' STRING_CONSTANT)? ')'
  ;

periodExpr
  :
  PERIOD_INDICATOR '(' expr? ')'
  ;

timeShiftExpr
  :
  TIMESHIFT '(' expr ',' INTEGER_CONSTANT ')'
  ;

timeSeriesExpr
  :
  FILL_TIME_SERIES '(' expr (',' (SINGLE|ALL))? ')'
  ;  
  
timeAggExpr
  :
  TIME_AGG '(' STRING_CONSTANT (',' (STRING_CONSTANT|'_'))? (',' (expr|'_'))? (',' (FIRST|LAST))? ')' 
  ;

validationExpr
  : CHECK '(' expr (erCode)? (erLevel)? (IMBALANCE expr)?  (INVALID|ALL)? ')'  
  ;
  
validationHierarchical
  :
  CHECK_HIERARCHY '(' expr',' IDENTIFIER (CONDITION componentID (',' componentID)*)? (RULE IDENTIFIER)? (NON_NULL|NON_ZERO|PARTIAL_NULL|PARTIAL_ZERO|ALWAYS_NULL|ALWAYS_ZERO)? (DATASET|DATASET_PRIORITY)? (INVALID|ALL|ALL_MEASURES)? ')'
  ;

hierarchyExpr
  : 
  HIERARCHY '(' expr ',' IDENTIFIER (CONDITION componentID (',' componentID)*)? (RULE IDENTIFIER)? ((NON_NULL|NON_ZERO|PARTIAL_NULL|PARTIAL_ZERO|ALWAYS_NULL|ALWAYS_ZERO)|'_')? ((RULE|DATASET|RULE_PRIORITY)|'_')? ((COMPUTED|ALL)|'_')? ')'
  ;

getFiltersClause
  :
    getFilterClause (',' getFilterClause)*
 ;

getFilterClause
  :
    (FILTER? expr)
  ;

inBetweenClause
  :
  IN (setExpr|IDENTIFIER)
  | NOT_IN (setExpr|IDENTIFIER)
  ;

subscriptExpr
  :
  persistentDatasetID '[' componentID '=' constant ( ',' componentID '=' constant)? ']' 
  ;
  
returnAll
  :
  RETURN ALL DATA POINTS
  ;

logBase
  :
  expr
  ;

exponent
  :
  INTEGER_CONSTANT|FLOAT_CONSTANT
  ;

persistentDatasetID
  : 
  STRING_CONSTANT
  ;
  
 operatorID
  :
  IDENTIFIER
  ;
  
  
 routineName
  :
  IDENTIFIER
  ; 
 
  componentType2
  :
  STRING
  | INTEGER
  | FLOAT
  | BOOLEAN
  | DATE
  ;
  
 scalarType
  :
  (basicScalarType|valueDomainName|setName)scalarTypeConstraint?((NOT)? NULL_CONSTANT)?
  ;
  
  basicScalarType
  :
  STRING
  | INTEGER
  | NUMBER
  | BOOLEAN
  | DATE
  | TIME_PERIOD
  | DURATION
  | SCALAR
  | TIME
  ;
  
  setName
  :
  IDENTIFIER
  ;
  
  scalarTypeConstraint
  :
  ('[' expr ']')
  |('{' constant (',' constant)* '}')
  ;
  
 dataType
  :
  scalarType
  |componentType
  |datasetType
  |scalarSetType
  |operatorType
  |rulesetType
  ;
  
  componentType
  :
  componentRole ('<' scalarType '>')?
  ;
  
  datasetType
  :
  DATASET ('{'compConstraint (',' compConstraint)* '}' )?
  ;
  
  compConstraint
  :
  componentType (componentID|multModifier)
  ;
  
  multModifier
  :
  '_' ('+'|'*')?
  ;
  
  rulesetType
  :
  RULESET
  |dpRuleset
  |hrRuleset
  ;
  
  dpRuleset
  :
  DATAPOINT
  |(DATAPOINT_ON_VD '{' prodValueDomains '}')
  |(DATAPOINT_ON_VAR '{' prodVariables '}')
  ;
  
  hrRuleset
  :
  HIERARCHICAL
  |(HIERARCHICAL_ON_VD ('{' IDENTIFIER ('('prodValueDomains')')? '}')? )
  |(HIERARCHICAL_ON_VAR ('{' varID ('('prodVariables')')? '}')? )
  ;
  
  prodValueDomains
  :
   IDENTIFIER ('*' IDENTIFIER)*
  ;
  
  prodVariables
  :
   varID ('*' varID)*
  ;
  
  operatorType
  :
  inputParameterType ('*' inputParameterType)* '->' outputParameterType
  ;
  
  inputParameterType
  :
  scalarType
  |datasetType
  |componentType
  ;
  
  outputParameterType
  :
  scalarType
  |datasetType
  |scalarSetType
  |rulesetType
  |componentType
  ;
  
  scalarSetType
  :
  SET ('<' scalarType '>')?
  ;
  
 defineDatapointRuleset
  :
  DEFINE DATAPOINT RULESET
  ;
  
 defineHierarchicalRuleset
   :
   DEFINE HIERARCHICAL RULESET
   ;
   
 endHierarchicalRuleset
   :
   END HIERARCHICAL RULESET
   ;
   
   
 defineDataStructure
   :
   DEFINE DATA STRUCTURE
   ; 
     
 
