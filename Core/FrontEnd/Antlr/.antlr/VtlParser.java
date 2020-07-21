// Generated from c:\Squadak\Projekty\Translator-VTL-EU\_Repo\StatisticsPoland.VtlProcessing.Core\trunk\Core\FrontEnd\Antlr\Vtl.g4 by ANTLR 4.7.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class VtlParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.7.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		ASSIGN=18, MEMBERSHIP=19, EVAL=20, IF=21, THEN=22, ELSE=23, USING=24, 
		WITH=25, CURRENT_DATE=26, ON=27, DROP=28, KEEP=29, CALC=30, ATTRCALC=31, 
		RENAME=32, AS=33, AND=34, OR=35, XOR=36, NOT=37, BETWEEN=38, IN=39, NOT_IN=40, 
		ISNULL=41, EX=42, UNION=43, DIFF=44, SYMDIFF=45, INTERSECT=46, KEYS=47, 
		CARTESIAN_PER=48, INTYEAR=49, INTMONTH=50, INTDAY=51, CHECK=52, EXISTS_IN=53, 
		TO=54, RETURN=55, IMBALANCE=56, ERRORCODE=57, ALL=58, AGGREGATE=59, ERRORLEVEL=60, 
		ORDER=61, BY=62, RANK=63, ASC=64, DESC=65, MIN=66, MAX=67, FIRST=68, LAST=69, 
		INDEXOF=70, ABS=71, KEY=72, LN=73, LOG=74, TRUNC=75, ROUND=76, POWER=77, 
		MOD=78, LEN=79, CONCAT=80, TRIM=81, UCASE=82, LCASE=83, SUBSTR=84, SUM=85, 
		AVG=86, MEDIAN=87, COUNT=88, DIMENSION=89, MEASURE=90, ATTRIBUTE=91, FILTER=92, 
		MERGE=93, EXP=94, ROLE=95, VIRAL=96, CHARSET_MATCH=97, TYPE=98, NVL=99, 
		HIERARCHY=100, OPTIONAL=101, INVALID=102, VALUE_DOMAIN=103, VARIABLE=104, 
		DATA=105, STRUCTURE=106, DATASET=107, OPERATOR=108, DEFINE=109, PUT_SYMBOL=110, 
		DATAPOINT=111, HIERARCHICAL=112, RULESET=113, RULE=114, END=115, ALTER_DATASET=116, 
		LTRIM=117, RTRIM=118, INSTR=119, REPLACE=120, CEIL=121, FLOOR=122, SQRT=123, 
		ANY=124, SETDIFF=125, STDDEV_POP=126, STDDEV_SAMP=127, VAR_POP=128, VAR_SAMP=129, 
		GROUP=130, EXCEPT=131, HAVING=132, FIRST_VALUE=133, LAST_VALUE=134, LAG=135, 
		LEAD=136, RATIO_TO_REPORT=137, OVER=138, PRECEDING=139, FOLLOWING=140, 
		UNBOUNDED=141, PARTITION=142, ROWS=143, RANGE=144, CURRENT=145, VALID=146, 
		FILL_TIME_SERIES=147, FLOW_TO_STOCK=148, STOCK_TO_FLOW=149, TIMESHIFT=150, 
		MEASURES=151, NO_MEASURES=152, CONDITION=153, BOOLEAN=154, DATE=155, TIME_PERIOD=156, 
		NUMBER=157, STRING=158, INTEGER=159, FLOAT=160, LIST=161, RECORD=162, 
		RESTRICT=163, YYYY=164, MM=165, DD=166, MAX_LENGTH=167, REGEXP=168, IS=169, 
		WHEN=170, FROM=171, AGGREGATES=172, POINTS=173, POINT=174, TOTAL=175, 
		PARTIAL=176, ALWAYS=177, INNER_JOIN=178, LEFT_JOIN=179, CROSS_JOIN=180, 
		FULL_JOIN=181, MAPS_FROM=182, MAPS_TO=183, MAP_TO=184, MAP_FROM=185, RETURNS=186, 
		PIVOT=187, UNPIVOT=188, SUBSPACE=189, APPLY=190, CONDITIONED=191, PERIOD_INDICATOR=192, 
		SINGLE=193, DURATION=194, TIME_AGG=195, UNIT=196, VALUE=197, VALUEDOMAINS=198, 
		VARIABLES=199, INPUT=200, OUTPUT=201, CAST=202, RULE_PRIORITY=203, DATASET_PRIORITY=204, 
		DEFAULT=205, CHECK_DATAPOINT=206, CHECK_HIERARCHY=207, COMPUTED=208, NON_NULL=209, 
		NON_ZERO=210, PARTIAL_NULL=211, PARTIAL_ZERO=212, ALWAYS_NULL=213, ALWAYS_ZERO=214, 
		COMPONENTS=215, ALL_MEASURES=216, SCALAR=217, COMPONENT=218, DATAPOINT_ON_VD=219, 
		DATAPOINT_ON_VAR=220, HIERARCHICAL_ON_VD=221, HIERARCHICAL_ON_VAR=222, 
		SET=223, LANGUAGE=224, INTEGER_CONSTANT=225, FLOAT_CONSTANT=226, FLOATEXP=227, 
		BOOLEAN_CONSTANT=228, NULL_CONSTANT=229, STRING_CONSTANT=230, TIME_CONSTANT=231, 
		IDENTIFIER=232, DIGITS0_9=233, MONTH=234, DAY=235, YEAR=236, WEEK=237, 
		HOURS=238, MINUTES=239, SECONDS=240, DATE_FORMAT=241, TIME_FORMAT=242, 
		TIME_UNIT=243, TIME=244, WS=245, EOL=246, ML_COMMENT=247, SL_COMMENT=248, 
		COMPARISON_OP=249, FREQUENCY=250;
	public static final int
		RULE_start = 0, RULE_statement = 1, RULE_dataset = 2, RULE_openedDataset = 3, 
		RULE_closedDataset = 4, RULE_membershipDataset = 5, RULE_datasetComplex = 6, 
		RULE_ifThenElseDataset = 7, RULE_unopenedDataset = 8, RULE_component = 9, 
		RULE_scalar = 10, RULE_ifThenElseScalar = 11, RULE_optionalExpr = 12, 
		RULE_setExpr = 13, RULE_datasetClause = 14, RULE_aggrClause = 15, RULE_aggrExpr = 16, 
		RULE_filterClause = 17, RULE_renameClause = 18, RULE_renameExpr = 19, 
		RULE_calcClause = 20, RULE_calcExpr = 21, RULE_keepClause = 22, RULE_dropClause = 23, 
		RULE_pivotClause = 24, RULE_unpivotClause = 25, RULE_subspaceClause = 26, 
		RULE_subspaceExpr = 27, RULE_joinExpr = 28, RULE_joinClause = 29, RULE_joinBody = 30, 
		RULE_joinAliasesClause = 31, RULE_joinAliasExpr = 32, RULE_joinUsingClause = 33, 
		RULE_joinCalcClause = 34, RULE_joinAggrClause = 35, RULE_joinKeepClause = 36, 
		RULE_joinDropClause = 37, RULE_joinFilterClause = 38, RULE_joinRenameClause = 39, 
		RULE_joinApplyClause = 40, RULE_aggrInvocation = 41, RULE_aggrFunction = 42, 
		RULE_aggrFunctionName = 43, RULE_groupingClause = 44, RULE_havingClause = 45, 
		RULE_havingExpr = 46, RULE_analyticInvocation = 47, RULE_analyticFunction = 48, 
		RULE_analyticClause = 49, RULE_partitionClause = 50, RULE_orderClause = 51, 
		RULE_orderExpr = 52, RULE_windowingClause = 53, RULE_firstWindowLimit = 54, 
		RULE_secondWindowLimit = 55, RULE_analyticFunctionName = 56, RULE_list = 57, 
		RULE_varID = 58, RULE_datasetID = 59, RULE_componentID = 60, RULE_joinKeyword = 61, 
		RULE_groupKeyword = 62, RULE_constant = 63, RULE_componentRole = 64, RULE_valueDomainName = 65, 
		RULE_retainType = 66, RULE_limitsMethod = 67;
	public static final String[] ruleNames = {
		"start", "statement", "dataset", "openedDataset", "closedDataset", "membershipDataset", 
		"datasetComplex", "ifThenElseDataset", "unopenedDataset", "component", 
		"scalar", "ifThenElseScalar", "optionalExpr", "setExpr", "datasetClause", 
		"aggrClause", "aggrExpr", "filterClause", "renameClause", "renameExpr", 
		"calcClause", "calcExpr", "keepClause", "dropClause", "pivotClause", "unpivotClause", 
		"subspaceClause", "subspaceExpr", "joinExpr", "joinClause", "joinBody", 
		"joinAliasesClause", "joinAliasExpr", "joinUsingClause", "joinCalcClause", 
		"joinAggrClause", "joinKeepClause", "joinDropClause", "joinFilterClause", 
		"joinRenameClause", "joinApplyClause", "aggrInvocation", "aggrFunction", 
		"aggrFunctionName", "groupingClause", "havingClause", "havingExpr", "analyticInvocation", 
		"analyticFunction", "analyticClause", "partitionClause", "orderClause", 
		"orderExpr", "windowingClause", "firstWindowLimit", "secondWindowLimit", 
		"analyticFunctionName", "list", "varID", "datasetID", "componentID", "joinKeyword", 
		"groupKeyword", "constant", "componentRole", "valueDomainName", "retainType", 
		"limitsMethod"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "'+'", "'-'", "'*'", "'/'", "'>'", "'<'", "'<='", "'>='", "'='", 
		"'<>'", "'['", "']'", "'('", "')'", "'{'", "'}'", "'\\'", "':='", "'#'", 
		"'eval'", "'if'", "'then'", "'else'", "'using'", "'with'", "'current_date'", 
		"'on'", "'drop'", "'keep'", "'calc'", "'attrcalc'", "'rename'", "'as'", 
		"'and'", "'or'", "'xor'", "'not'", "'between'", "'in'", "'not_in'", "'isnull'", 
		"'ex'", "'union'", "'diff'", "'symdiff'", "'intersect'", "'keys'", "','", 
		"'intyear'", "'intmonth'", "'intday'", "'check'", "'exists_in'", "'to'", 
		"'return'", "'imbalance'", "'errorcode'", "'all'", "'aggr'", "'errorlevel'", 
		"'order'", "'by'", "'rank'", "'asc'", "'desc'", "'min'", "'max'", "'first'", 
		"'last'", "'indexof'", "'abs'", "'key'", "'ln'", "'log'", "'trunc'", "'round'", 
		"'power'", "'mod'", "'length'", "'||'", "'trim'", "'upper'", "'lower'", 
		"'substr'", "'sum'", "'avg'", "'median'", "'count'", "'identifier'", "'measure'", 
		"'attribute'", "'filter'", "'merge'", "'exp'", "'role'", "'viral'", "'match_characters'", 
		"'type'", "'nvl'", "'hierarchy'", "'_'", "'invalid'", "'valuedomain'", 
		"'variable'", "'data'", "'structure'", "'dataset'", "'operator'", "'define'", 
		"'<-'", "'datapoint'", "'hierarchical'", "'ruleset'", "'rule'", "'end'", 
		"'alterDataset'", "'ltrim'", "'rtrim'", "'instr'", "'replace'", "'ceil'", 
		"'floor'", "'sqrt'", "'any'", "'setdiff'", "'stddev_pop'", "'stddev_samp'", 
		"'var_pop'", "'var_samp'", "'group'", "'except'", "'having'", "'first_value'", 
		"'last_value'", "'lag'", "'lead'", "'ratio_to_report'", "'over'", "'preceding'", 
		"'following'", "'unbounded'", "'partition'", "'rows'", "'range'", "'current'", 
		"'valid'", "'fill_time_series'", "'flow_to_stock'", "'stock_to_flow'", 
		"'timeshift'", "'measures'", "'no_measures'", "'condition'", "'boolean'", 
		"'date'", "'time_period'", "'number'", "'string'", "'integer'", "'float'", 
		"'list'", "'record'", "'restrict'", "'yyyy'", "'mm'", "'dd'", "'maxLength'", 
		"'regexp'", "'is'", "'when'", "'from'", "'aggregates'", "'points'", "'point'", 
		"'total'", "'partial'", "'always'", "'inner_join'", "'left_join'", "'cross_join'", 
		"'full_join'", "'maps_from'", "'maps_to'", "'map_to'", "'map_from'", "'returns'", 
		"'pivot'", "'unpivot'", "'sub'", "'apply'", "'conditioned'", "'period_indicator'", 
		"'single'", "'duration'", "'time_agg'", "'unit'", "'Value'", "'valuedomains'", 
		"'variables'", "'input'", "'output'", "'cast'", "'rule_priority'", "'dataset_priority'", 
		"'default'", "'check_datapoint'", "'check_hierarchy'", "'computed'", "'non_null'", 
		"'non_zero'", "'partial_null'", "'partial_zero'", "'always_null'", "'always_zero'", 
		"'components'", "'all_measures'", "'scalar'", "'component'", "'datapoint_on_valuedomains'", 
		"'datapoint_on_variables'", "'hierarchical_on_valuedomains'", "'hierarchical_on_variables'", 
		"'set'", "'language'", null, null, null, null, "'null'", null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, "';'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, "ASSIGN", "MEMBERSHIP", "EVAL", "IF", 
		"THEN", "ELSE", "USING", "WITH", "CURRENT_DATE", "ON", "DROP", "KEEP", 
		"CALC", "ATTRCALC", "RENAME", "AS", "AND", "OR", "XOR", "NOT", "BETWEEN", 
		"IN", "NOT_IN", "ISNULL", "EX", "UNION", "DIFF", "SYMDIFF", "INTERSECT", 
		"KEYS", "CARTESIAN_PER", "INTYEAR", "INTMONTH", "INTDAY", "CHECK", "EXISTS_IN", 
		"TO", "RETURN", "IMBALANCE", "ERRORCODE", "ALL", "AGGREGATE", "ERRORLEVEL", 
		"ORDER", "BY", "RANK", "ASC", "DESC", "MIN", "MAX", "FIRST", "LAST", "INDEXOF", 
		"ABS", "KEY", "LN", "LOG", "TRUNC", "ROUND", "POWER", "MOD", "LEN", "CONCAT", 
		"TRIM", "UCASE", "LCASE", "SUBSTR", "SUM", "AVG", "MEDIAN", "COUNT", "DIMENSION", 
		"MEASURE", "ATTRIBUTE", "FILTER", "MERGE", "EXP", "ROLE", "VIRAL", "CHARSET_MATCH", 
		"TYPE", "NVL", "HIERARCHY", "OPTIONAL", "INVALID", "VALUE_DOMAIN", "VARIABLE", 
		"DATA", "STRUCTURE", "DATASET", "OPERATOR", "DEFINE", "PUT_SYMBOL", "DATAPOINT", 
		"HIERARCHICAL", "RULESET", "RULE", "END", "ALTER_DATASET", "LTRIM", "RTRIM", 
		"INSTR", "REPLACE", "CEIL", "FLOOR", "SQRT", "ANY", "SETDIFF", "STDDEV_POP", 
		"STDDEV_SAMP", "VAR_POP", "VAR_SAMP", "GROUP", "EXCEPT", "HAVING", "FIRST_VALUE", 
		"LAST_VALUE", "LAG", "LEAD", "RATIO_TO_REPORT", "OVER", "PRECEDING", "FOLLOWING", 
		"UNBOUNDED", "PARTITION", "ROWS", "RANGE", "CURRENT", "VALID", "FILL_TIME_SERIES", 
		"FLOW_TO_STOCK", "STOCK_TO_FLOW", "TIMESHIFT", "MEASURES", "NO_MEASURES", 
		"CONDITION", "BOOLEAN", "DATE", "TIME_PERIOD", "NUMBER", "STRING", "INTEGER", 
		"FLOAT", "LIST", "RECORD", "RESTRICT", "YYYY", "MM", "DD", "MAX_LENGTH", 
		"REGEXP", "IS", "WHEN", "FROM", "AGGREGATES", "POINTS", "POINT", "TOTAL", 
		"PARTIAL", "ALWAYS", "INNER_JOIN", "LEFT_JOIN", "CROSS_JOIN", "FULL_JOIN", 
		"MAPS_FROM", "MAPS_TO", "MAP_TO", "MAP_FROM", "RETURNS", "PIVOT", "UNPIVOT", 
		"SUBSPACE", "APPLY", "CONDITIONED", "PERIOD_INDICATOR", "SINGLE", "DURATION", 
		"TIME_AGG", "UNIT", "VALUE", "VALUEDOMAINS", "VARIABLES", "INPUT", "OUTPUT", 
		"CAST", "RULE_PRIORITY", "DATASET_PRIORITY", "DEFAULT", "CHECK_DATAPOINT", 
		"CHECK_HIERARCHY", "COMPUTED", "NON_NULL", "NON_ZERO", "PARTIAL_NULL", 
		"PARTIAL_ZERO", "ALWAYS_NULL", "ALWAYS_ZERO", "COMPONENTS", "ALL_MEASURES", 
		"SCALAR", "COMPONENT", "DATAPOINT_ON_VD", "DATAPOINT_ON_VAR", "HIERARCHICAL_ON_VD", 
		"HIERARCHICAL_ON_VAR", "SET", "LANGUAGE", "INTEGER_CONSTANT", "FLOAT_CONSTANT", 
		"FLOATEXP", "BOOLEAN_CONSTANT", "NULL_CONSTANT", "STRING_CONSTANT", "TIME_CONSTANT", 
		"IDENTIFIER", "DIGITS0_9", "MONTH", "DAY", "YEAR", "WEEK", "HOURS", "MINUTES", 
		"SECONDS", "DATE_FORMAT", "TIME_FORMAT", "TIME_UNIT", "TIME", "WS", "EOL", 
		"ML_COMMENT", "SL_COMMENT", "COMPARISON_OP", "FREQUENCY"
	};
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "Vtl.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public VtlParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}
	public static class StartContext extends ParserRuleContext {
		public TerminalNode EOF() { return getToken(VtlParser.EOF, 0); }
		public List<TerminalNode> EOL() { return getTokens(VtlParser.EOL); }
		public TerminalNode EOL(int i) {
			return getToken(VtlParser.EOL, i);
		}
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public List<TerminalNode> ML_COMMENT() { return getTokens(VtlParser.ML_COMMENT); }
		public TerminalNode ML_COMMENT(int i) {
			return getToken(VtlParser.ML_COMMENT, i);
		}
		public List<TerminalNode> SL_COMMENT() { return getTokens(VtlParser.SL_COMMENT); }
		public TerminalNode SL_COMMENT(int i) {
			return getToken(VtlParser.SL_COMMENT, i);
		}
		public StartContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_start; }
	}

	public final StartContext start() throws RecognitionException {
		StartContext _localctx = new StartContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_start);
		int _la;
		try {
			int _alt;
			setState(163);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,5,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(154);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,3,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(137);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__12) | (1L << IF) | (1L << CURRENT_DATE) | (1L << NOT) | (1L << BETWEEN) | (1L << ISNULL) | (1L << UNION) | (1L << SYMDIFF) | (1L << INTERSECT) | (1L << EXISTS_IN))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (MIN - 66)) | (1L << (MAX - 66)) | (1L << (ABS - 66)) | (1L << (LN - 66)) | (1L << (LOG - 66)) | (1L << (TRUNC - 66)) | (1L << (ROUND - 66)) | (1L << (POWER - 66)) | (1L << (MOD - 66)) | (1L << (LEN - 66)) | (1L << (TRIM - 66)) | (1L << (UCASE - 66)) | (1L << (LCASE - 66)) | (1L << (SUBSTR - 66)) | (1L << (SUM - 66)) | (1L << (AVG - 66)) | (1L << (MEDIAN - 66)) | (1L << (COUNT - 66)) | (1L << (EXP - 66)) | (1L << (CHARSET_MATCH - 66)) | (1L << (NVL - 66)) | (1L << (LTRIM - 66)) | (1L << (RTRIM - 66)) | (1L << (INSTR - 66)) | (1L << (REPLACE - 66)) | (1L << (CEIL - 66)) | (1L << (FLOOR - 66)) | (1L << (SQRT - 66)) | (1L << (SETDIFF - 66)) | (1L << (STDDEV_POP - 66)) | (1L << (STDDEV_SAMP - 66)) | (1L << (VAR_POP - 66)) | (1L << (VAR_SAMP - 66)))) != 0) || ((((_la - 133)) & ~0x3f) == 0 && ((1L << (_la - 133)) & ((1L << (FIRST_VALUE - 133)) | (1L << (LAST_VALUE - 133)) | (1L << (LAG - 133)) | (1L << (LEAD - 133)) | (1L << (RATIO_TO_REPORT - 133)) | (1L << (FILL_TIME_SERIES - 133)) | (1L << (FLOW_TO_STOCK - 133)) | (1L << (STOCK_TO_FLOW - 133)) | (1L << (TIMESHIFT - 133)) | (1L << (INNER_JOIN - 133)) | (1L << (LEFT_JOIN - 133)) | (1L << (CROSS_JOIN - 133)) | (1L << (FULL_JOIN - 133)) | (1L << (PERIOD_INDICATOR - 133)))) != 0) || ((((_la - 225)) & ~0x3f) == 0 && ((1L << (_la - 225)) & ((1L << (INTEGER_CONSTANT - 225)) | (1L << (FLOAT_CONSTANT - 225)) | (1L << (BOOLEAN_CONSTANT - 225)) | (1L << (NULL_CONSTANT - 225)) | (1L << (STRING_CONSTANT - 225)) | (1L << (TIME_CONSTANT - 225)) | (1L << (IDENTIFIER - 225)))) != 0)) {
							{
							setState(136);
							statement();
							}
						}

						setState(142);
						_errHandler.sync(this);
						_la = _input.LA(1);
						while (_la==ML_COMMENT) {
							{
							{
							setState(139);
							match(ML_COMMENT);
							}
							}
							setState(144);
							_errHandler.sync(this);
							_la = _input.LA(1);
						}
						setState(148);
						_errHandler.sync(this);
						_la = _input.LA(1);
						while (_la==SL_COMMENT) {
							{
							{
							setState(145);
							match(SL_COMMENT);
							}
							}
							setState(150);
							_errHandler.sync(this);
							_la = _input.LA(1);
						}
						setState(151);
						match(EOL);
						}
						} 
					}
					setState(156);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,3,_ctx);
				}
				setState(158);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__12) | (1L << IF) | (1L << CURRENT_DATE) | (1L << NOT) | (1L << BETWEEN) | (1L << ISNULL) | (1L << UNION) | (1L << SYMDIFF) | (1L << INTERSECT) | (1L << EXISTS_IN))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (MIN - 66)) | (1L << (MAX - 66)) | (1L << (ABS - 66)) | (1L << (LN - 66)) | (1L << (LOG - 66)) | (1L << (TRUNC - 66)) | (1L << (ROUND - 66)) | (1L << (POWER - 66)) | (1L << (MOD - 66)) | (1L << (LEN - 66)) | (1L << (TRIM - 66)) | (1L << (UCASE - 66)) | (1L << (LCASE - 66)) | (1L << (SUBSTR - 66)) | (1L << (SUM - 66)) | (1L << (AVG - 66)) | (1L << (MEDIAN - 66)) | (1L << (COUNT - 66)) | (1L << (EXP - 66)) | (1L << (CHARSET_MATCH - 66)) | (1L << (NVL - 66)) | (1L << (LTRIM - 66)) | (1L << (RTRIM - 66)) | (1L << (INSTR - 66)) | (1L << (REPLACE - 66)) | (1L << (CEIL - 66)) | (1L << (FLOOR - 66)) | (1L << (SQRT - 66)) | (1L << (SETDIFF - 66)) | (1L << (STDDEV_POP - 66)) | (1L << (STDDEV_SAMP - 66)) | (1L << (VAR_POP - 66)) | (1L << (VAR_SAMP - 66)))) != 0) || ((((_la - 133)) & ~0x3f) == 0 && ((1L << (_la - 133)) & ((1L << (FIRST_VALUE - 133)) | (1L << (LAST_VALUE - 133)) | (1L << (LAG - 133)) | (1L << (LEAD - 133)) | (1L << (RATIO_TO_REPORT - 133)) | (1L << (FILL_TIME_SERIES - 133)) | (1L << (FLOW_TO_STOCK - 133)) | (1L << (STOCK_TO_FLOW - 133)) | (1L << (TIMESHIFT - 133)) | (1L << (INNER_JOIN - 133)) | (1L << (LEFT_JOIN - 133)) | (1L << (CROSS_JOIN - 133)) | (1L << (FULL_JOIN - 133)) | (1L << (PERIOD_INDICATOR - 133)))) != 0) || ((((_la - 225)) & ~0x3f) == 0 && ((1L << (_la - 225)) & ((1L << (INTEGER_CONSTANT - 225)) | (1L << (FLOAT_CONSTANT - 225)) | (1L << (BOOLEAN_CONSTANT - 225)) | (1L << (NULL_CONSTANT - 225)) | (1L << (STRING_CONSTANT - 225)) | (1L << (TIME_CONSTANT - 225)) | (1L << (IDENTIFIER - 225)))) != 0)) {
					{
					setState(157);
					statement();
					}
				}

				setState(160);
				match(EOF);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(161);
				match(ML_COMMENT);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(162);
				match(SL_COMMENT);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StatementContext extends ParserRuleContext {
		public DatasetContext dataset() {
			return getRuleContext(DatasetContext.class,0);
		}
		public ScalarContext scalar() {
			return getRuleContext(ScalarContext.class,0);
		}
		public VarIDContext varID() {
			return getRuleContext(VarIDContext.class,0);
		}
		public TerminalNode ASSIGN() { return getToken(VtlParser.ASSIGN, 0); }
		public TerminalNode PUT_SYMBOL() { return getToken(VtlParser.PUT_SYMBOL, 0); }
		public StatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_statement; }
	}

	public final StatementContext statement() throws RecognitionException {
		StatementContext _localctx = new StatementContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_statement);
		try {
			setState(178);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,8,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(168);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,6,_ctx) ) {
				case 1:
					{
					setState(165);
					varID();
					setState(166);
					match(ASSIGN);
					}
					break;
				}
				setState(172);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,7,_ctx) ) {
				case 1:
					{
					setState(170);
					dataset();
					}
					break;
				case 2:
					{
					setState(171);
					scalar(0);
					}
					break;
				}
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(174);
				varID();
				setState(175);
				match(PUT_SYMBOL);
				setState(176);
				dataset();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DatasetContext extends ParserRuleContext {
		public ClosedDatasetContext closedDataset() {
			return getRuleContext(ClosedDatasetContext.class,0);
		}
		public MembershipDatasetContext membershipDataset() {
			return getRuleContext(MembershipDatasetContext.class,0);
		}
		public OpenedDatasetContext openedDataset() {
			return getRuleContext(OpenedDatasetContext.class,0);
		}
		public DatasetContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_dataset; }
	}

	public final DatasetContext dataset() throws RecognitionException {
		DatasetContext _localctx = new DatasetContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_dataset);
		try {
			setState(183);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,9,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(180);
				closedDataset(0);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(181);
				membershipDataset();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(182);
				openedDataset(0);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class OpenedDatasetContext extends ParserRuleContext {
		public OpenedDatasetContext openedDatasetLeft;
		public Token opSymbol;
		public IfThenElseDatasetContext ifThenElseDataset() {
			return getRuleContext(IfThenElseDatasetContext.class,0);
		}
		public DatasetContext dataset() {
			return getRuleContext(DatasetContext.class,0);
		}
		public TerminalNode NOT() { return getToken(VtlParser.NOT, 0); }
		public UnopenedDatasetContext unopenedDataset() {
			return getRuleContext(UnopenedDatasetContext.class,0);
		}
		public List<OpenedDatasetContext> openedDataset() {
			return getRuleContexts(OpenedDatasetContext.class);
		}
		public OpenedDatasetContext openedDataset(int i) {
			return getRuleContext(OpenedDatasetContext.class,i);
		}
		public ClosedDatasetContext closedDataset() {
			return getRuleContext(ClosedDatasetContext.class,0);
		}
		public MembershipDatasetContext membershipDataset() {
			return getRuleContext(MembershipDatasetContext.class,0);
		}
		public ScalarContext scalar() {
			return getRuleContext(ScalarContext.class,0);
		}
		public ConstantContext constant() {
			return getRuleContext(ConstantContext.class,0);
		}
		public TerminalNode AND() { return getToken(VtlParser.AND, 0); }
		public TerminalNode OR() { return getToken(VtlParser.OR, 0); }
		public TerminalNode XOR() { return getToken(VtlParser.XOR, 0); }
		public TerminalNode CONCAT() { return getToken(VtlParser.CONCAT, 0); }
		public TerminalNode IN() { return getToken(VtlParser.IN, 0); }
		public TerminalNode NOT_IN() { return getToken(VtlParser.NOT_IN, 0); }
		public ListContext list() {
			return getRuleContext(ListContext.class,0);
		}
		public ValueDomainNameContext valueDomainName() {
			return getRuleContext(ValueDomainNameContext.class,0);
		}
		public OpenedDatasetContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_openedDataset; }
	}

	public final OpenedDatasetContext openedDataset() throws RecognitionException {
		return openedDataset(0);
	}

	private OpenedDatasetContext openedDataset(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		OpenedDatasetContext _localctx = new OpenedDatasetContext(_ctx, _parentState);
		OpenedDatasetContext _prevctx = _localctx;
		int _startState = 6;
		enterRecursionRule(_localctx, 6, RULE_openedDataset, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(269);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,23,_ctx) ) {
			case 1:
				{
				setState(186);
				ifThenElseDataset();
				}
				break;
			case 2:
				{
				setState(187);
				((OpenedDatasetContext)_localctx).opSymbol = match(NOT);
				setState(188);
				dataset();
				}
				break;
			case 3:
				{
				setState(189);
				unopenedDataset();
				setState(190);
				((OpenedDatasetContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !(_la==T__0 || _la==T__1) ) {
					((OpenedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(195);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,10,_ctx) ) {
				case 1:
					{
					setState(191);
					openedDataset(0);
					}
					break;
				case 2:
					{
					setState(192);
					closedDataset(0);
					}
					break;
				case 3:
					{
					setState(193);
					membershipDataset();
					}
					break;
				case 4:
					{
					setState(194);
					scalar(0);
					}
					break;
				}
				}
				break;
			case 4:
				{
				setState(197);
				unopenedDataset();
				setState(198);
				((OpenedDatasetContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !(_la==T__2 || _la==T__3) ) {
					((OpenedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(204);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,11,_ctx) ) {
				case 1:
					{
					setState(199);
					closedDataset(0);
					}
					break;
				case 2:
					{
					setState(200);
					membershipDataset();
					}
					break;
				case 3:
					{
					setState(201);
					constant();
					}
					break;
				case 4:
					{
					setState(202);
					scalar(0);
					}
					break;
				case 5:
					{
					setState(203);
					openedDataset(0);
					}
					break;
				}
				}
				break;
			case 5:
				{
				setState(206);
				unopenedDataset();
				setState(207);
				((OpenedDatasetContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !(((((_la - 34)) & ~0x3f) == 0 && ((1L << (_la - 34)) & ((1L << (AND - 34)) | (1L << (OR - 34)) | (1L << (XOR - 34)) | (1L << (CONCAT - 34)))) != 0)) ) {
					((OpenedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(212);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,12,_ctx) ) {
				case 1:
					{
					setState(208);
					openedDataset(0);
					}
					break;
				case 2:
					{
					setState(209);
					closedDataset(0);
					}
					break;
				case 3:
					{
					setState(210);
					membershipDataset();
					}
					break;
				case 4:
					{
					setState(211);
					scalar(0);
					}
					break;
				}
				}
				break;
			case 6:
				{
				setState(214);
				unopenedDataset();
				setState(215);
				((OpenedDatasetContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__4) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__9))) != 0)) ) {
					((OpenedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(221);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,13,_ctx) ) {
				case 1:
					{
					setState(216);
					closedDataset(0);
					}
					break;
				case 2:
					{
					setState(217);
					membershipDataset();
					}
					break;
				case 3:
					{
					setState(218);
					constant();
					}
					break;
				case 4:
					{
					setState(219);
					scalar(0);
					}
					break;
				case 5:
					{
					setState(220);
					openedDataset(0);
					}
					break;
				}
				}
				break;
			case 7:
				{
				setState(223);
				unopenedDataset();
				setState(224);
				((OpenedDatasetContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !(_la==IN || _la==NOT_IN) ) {
					((OpenedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(227);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case T__14:
					{
					setState(225);
					list();
					}
					break;
				case IDENTIFIER:
					{
					setState(226);
					valueDomainName();
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				break;
			case 8:
				{
				setState(231);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,15,_ctx) ) {
				case 1:
					{
					setState(229);
					constant();
					}
					break;
				case 2:
					{
					setState(230);
					scalar(0);
					}
					break;
				}
				setState(233);
				((OpenedDatasetContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !(_la==T__0 || _la==T__1) ) {
					((OpenedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(237);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,16,_ctx) ) {
				case 1:
					{
					setState(234);
					openedDataset(0);
					}
					break;
				case 2:
					{
					setState(235);
					closedDataset(0);
					}
					break;
				case 3:
					{
					setState(236);
					membershipDataset();
					}
					break;
				}
				}
				break;
			case 9:
				{
				setState(241);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,17,_ctx) ) {
				case 1:
					{
					setState(239);
					constant();
					}
					break;
				case 2:
					{
					setState(240);
					scalar(0);
					}
					break;
				}
				setState(243);
				((OpenedDatasetContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !(_la==T__2 || _la==T__3) ) {
					((OpenedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(247);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,18,_ctx) ) {
				case 1:
					{
					setState(244);
					closedDataset(0);
					}
					break;
				case 2:
					{
					setState(245);
					membershipDataset();
					}
					break;
				case 3:
					{
					setState(246);
					openedDataset(0);
					}
					break;
				}
				}
				break;
			case 10:
				{
				setState(251);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,19,_ctx) ) {
				case 1:
					{
					setState(249);
					constant();
					}
					break;
				case 2:
					{
					setState(250);
					scalar(0);
					}
					break;
				}
				setState(253);
				((OpenedDatasetContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !(((((_la - 34)) & ~0x3f) == 0 && ((1L << (_la - 34)) & ((1L << (AND - 34)) | (1L << (OR - 34)) | (1L << (XOR - 34)) | (1L << (CONCAT - 34)))) != 0)) ) {
					((OpenedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(257);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,20,_ctx) ) {
				case 1:
					{
					setState(254);
					openedDataset(0);
					}
					break;
				case 2:
					{
					setState(255);
					closedDataset(0);
					}
					break;
				case 3:
					{
					setState(256);
					membershipDataset();
					}
					break;
				}
				}
				break;
			case 11:
				{
				setState(261);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,21,_ctx) ) {
				case 1:
					{
					setState(259);
					constant();
					}
					break;
				case 2:
					{
					setState(260);
					scalar(0);
					}
					break;
				}
				setState(263);
				((OpenedDatasetContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__4) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__9))) != 0)) ) {
					((OpenedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(267);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,22,_ctx) ) {
				case 1:
					{
					setState(264);
					closedDataset(0);
					}
					break;
				case 2:
					{
					setState(265);
					membershipDataset();
					}
					break;
				case 3:
					{
					setState(266);
					openedDataset(0);
					}
					break;
				}
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(313);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,30,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(311);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,29,_ctx) ) {
					case 1:
						{
						_localctx = new OpenedDatasetContext(_parentctx, _parentState);
						_localctx.openedDatasetLeft = _prevctx;
						_localctx.openedDatasetLeft = _prevctx;
						pushNewRecursionContext(_localctx, _startState, RULE_openedDataset);
						setState(271);
						if (!(precpred(_ctx, 9))) throw new FailedPredicateException(this, "precpred(_ctx, 9)");
						setState(272);
						((OpenedDatasetContext)_localctx).opSymbol = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==T__0 || _la==T__1) ) {
							((OpenedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(277);
						_errHandler.sync(this);
						switch ( getInterpreter().adaptivePredict(_input,24,_ctx) ) {
						case 1:
							{
							setState(273);
							openedDataset(0);
							}
							break;
						case 2:
							{
							setState(274);
							closedDataset(0);
							}
							break;
						case 3:
							{
							setState(275);
							membershipDataset();
							}
							break;
						case 4:
							{
							setState(276);
							scalar(0);
							}
							break;
						}
						}
						break;
					case 2:
						{
						_localctx = new OpenedDatasetContext(_parentctx, _parentState);
						_localctx.openedDatasetLeft = _prevctx;
						_localctx.openedDatasetLeft = _prevctx;
						pushNewRecursionContext(_localctx, _startState, RULE_openedDataset);
						setState(279);
						if (!(precpred(_ctx, 8))) throw new FailedPredicateException(this, "precpred(_ctx, 8)");
						setState(280);
						((OpenedDatasetContext)_localctx).opSymbol = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==T__2 || _la==T__3) ) {
							((OpenedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(286);
						_errHandler.sync(this);
						switch ( getInterpreter().adaptivePredict(_input,25,_ctx) ) {
						case 1:
							{
							setState(281);
							closedDataset(0);
							}
							break;
						case 2:
							{
							setState(282);
							membershipDataset();
							}
							break;
						case 3:
							{
							setState(283);
							constant();
							}
							break;
						case 4:
							{
							setState(284);
							scalar(0);
							}
							break;
						case 5:
							{
							setState(285);
							openedDataset(0);
							}
							break;
						}
						}
						break;
					case 3:
						{
						_localctx = new OpenedDatasetContext(_parentctx, _parentState);
						_localctx.openedDatasetLeft = _prevctx;
						_localctx.openedDatasetLeft = _prevctx;
						pushNewRecursionContext(_localctx, _startState, RULE_openedDataset);
						setState(288);
						if (!(precpred(_ctx, 7))) throw new FailedPredicateException(this, "precpred(_ctx, 7)");
						setState(289);
						((OpenedDatasetContext)_localctx).opSymbol = _input.LT(1);
						_la = _input.LA(1);
						if ( !(((((_la - 34)) & ~0x3f) == 0 && ((1L << (_la - 34)) & ((1L << (AND - 34)) | (1L << (OR - 34)) | (1L << (XOR - 34)) | (1L << (CONCAT - 34)))) != 0)) ) {
							((OpenedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(294);
						_errHandler.sync(this);
						switch ( getInterpreter().adaptivePredict(_input,26,_ctx) ) {
						case 1:
							{
							setState(290);
							openedDataset(0);
							}
							break;
						case 2:
							{
							setState(291);
							closedDataset(0);
							}
							break;
						case 3:
							{
							setState(292);
							membershipDataset();
							}
							break;
						case 4:
							{
							setState(293);
							scalar(0);
							}
							break;
						}
						}
						break;
					case 4:
						{
						_localctx = new OpenedDatasetContext(_parentctx, _parentState);
						_localctx.openedDatasetLeft = _prevctx;
						_localctx.openedDatasetLeft = _prevctx;
						pushNewRecursionContext(_localctx, _startState, RULE_openedDataset);
						setState(296);
						if (!(precpred(_ctx, 6))) throw new FailedPredicateException(this, "precpred(_ctx, 6)");
						setState(297);
						((OpenedDatasetContext)_localctx).opSymbol = _input.LT(1);
						_la = _input.LA(1);
						if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__4) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__9))) != 0)) ) {
							((OpenedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(303);
						_errHandler.sync(this);
						switch ( getInterpreter().adaptivePredict(_input,27,_ctx) ) {
						case 1:
							{
							setState(298);
							closedDataset(0);
							}
							break;
						case 2:
							{
							setState(299);
							membershipDataset();
							}
							break;
						case 3:
							{
							setState(300);
							constant();
							}
							break;
						case 4:
							{
							setState(301);
							scalar(0);
							}
							break;
						case 5:
							{
							setState(302);
							openedDataset(0);
							}
							break;
						}
						}
						break;
					case 5:
						{
						_localctx = new OpenedDatasetContext(_parentctx, _parentState);
						_localctx.openedDatasetLeft = _prevctx;
						_localctx.openedDatasetLeft = _prevctx;
						pushNewRecursionContext(_localctx, _startState, RULE_openedDataset);
						setState(305);
						if (!(precpred(_ctx, 5))) throw new FailedPredicateException(this, "precpred(_ctx, 5)");
						setState(306);
						((OpenedDatasetContext)_localctx).opSymbol = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==IN || _la==NOT_IN) ) {
							((OpenedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(309);
						_errHandler.sync(this);
						switch (_input.LA(1)) {
						case T__14:
							{
							setState(307);
							list();
							}
							break;
						case IDENTIFIER:
							{
							setState(308);
							valueDomainName();
							}
							break;
						default:
							throw new NoViableAltException(this);
						}
						}
						break;
					}
					} 
				}
				setState(315);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,30,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class ClosedDatasetContext extends ParserRuleContext {
		public Token opSymbol;
		public DatasetIDContext datasetID() {
			return getRuleContext(DatasetIDContext.class,0);
		}
		public DatasetComplexContext datasetComplex() {
			return getRuleContext(DatasetComplexContext.class,0);
		}
		public ClosedDatasetContext closedDataset() {
			return getRuleContext(ClosedDatasetContext.class,0);
		}
		public MembershipDatasetContext membershipDataset() {
			return getRuleContext(MembershipDatasetContext.class,0);
		}
		public OpenedDatasetContext openedDataset() {
			return getRuleContext(OpenedDatasetContext.class,0);
		}
		public List<DatasetContext> dataset() {
			return getRuleContexts(DatasetContext.class);
		}
		public DatasetContext dataset(int i) {
			return getRuleContext(DatasetContext.class,i);
		}
		public TerminalNode ROUND() { return getToken(VtlParser.ROUND, 0); }
		public List<OptionalExprContext> optionalExpr() {
			return getRuleContexts(OptionalExprContext.class);
		}
		public OptionalExprContext optionalExpr(int i) {
			return getRuleContext(OptionalExprContext.class,i);
		}
		public TerminalNode CEIL() { return getToken(VtlParser.CEIL, 0); }
		public TerminalNode FLOOR() { return getToken(VtlParser.FLOOR, 0); }
		public TerminalNode ABS() { return getToken(VtlParser.ABS, 0); }
		public TerminalNode EXP() { return getToken(VtlParser.EXP, 0); }
		public TerminalNode LN() { return getToken(VtlParser.LN, 0); }
		public List<ScalarContext> scalar() {
			return getRuleContexts(ScalarContext.class);
		}
		public ScalarContext scalar(int i) {
			return getRuleContext(ScalarContext.class,i);
		}
		public TerminalNode LOG() { return getToken(VtlParser.LOG, 0); }
		public TerminalNode TRUNC() { return getToken(VtlParser.TRUNC, 0); }
		public TerminalNode POWER() { return getToken(VtlParser.POWER, 0); }
		public TerminalNode SQRT() { return getToken(VtlParser.SQRT, 0); }
		public TerminalNode LEN() { return getToken(VtlParser.LEN, 0); }
		public TerminalNode BETWEEN() { return getToken(VtlParser.BETWEEN, 0); }
		public TerminalNode TRIM() { return getToken(VtlParser.TRIM, 0); }
		public TerminalNode LTRIM() { return getToken(VtlParser.LTRIM, 0); }
		public TerminalNode RTRIM() { return getToken(VtlParser.RTRIM, 0); }
		public TerminalNode UCASE() { return getToken(VtlParser.UCASE, 0); }
		public TerminalNode LCASE() { return getToken(VtlParser.LCASE, 0); }
		public TerminalNode SUBSTR() { return getToken(VtlParser.SUBSTR, 0); }
		public TerminalNode INSTR() { return getToken(VtlParser.INSTR, 0); }
		public TerminalNode REPLACE() { return getToken(VtlParser.REPLACE, 0); }
		public TerminalNode CHARSET_MATCH() { return getToken(VtlParser.CHARSET_MATCH, 0); }
		public TerminalNode ISNULL() { return getToken(VtlParser.ISNULL, 0); }
		public TerminalNode NVL() { return getToken(VtlParser.NVL, 0); }
		public TerminalNode MOD() { return getToken(VtlParser.MOD, 0); }
		public TerminalNode EXISTS_IN() { return getToken(VtlParser.EXISTS_IN, 0); }
		public RetainTypeContext retainType() {
			return getRuleContext(RetainTypeContext.class,0);
		}
		public TerminalNode FLOW_TO_STOCK() { return getToken(VtlParser.FLOW_TO_STOCK, 0); }
		public TerminalNode STOCK_TO_FLOW() { return getToken(VtlParser.STOCK_TO_FLOW, 0); }
		public TerminalNode PERIOD_INDICATOR() { return getToken(VtlParser.PERIOD_INDICATOR, 0); }
		public TerminalNode TIMESHIFT() { return getToken(VtlParser.TIMESHIFT, 0); }
		public TerminalNode FILL_TIME_SERIES() { return getToken(VtlParser.FILL_TIME_SERIES, 0); }
		public LimitsMethodContext limitsMethod() {
			return getRuleContext(LimitsMethodContext.class,0);
		}
		public DatasetClauseContext datasetClause() {
			return getRuleContext(DatasetClauseContext.class,0);
		}
		public ClosedDatasetContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_closedDataset; }
	}

	public final ClosedDatasetContext closedDataset() throws RecognitionException {
		return closedDataset(0);
	}

	private ClosedDatasetContext closedDataset(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ClosedDatasetContext _localctx = new ClosedDatasetContext(_ctx, _parentState);
		ClosedDatasetContext _prevctx = _localctx;
		int _startState = 8;
		enterRecursionRule(_localctx, 8, RULE_closedDataset, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(551);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,42,_ctx) ) {
			case 1:
				{
				setState(317);
				datasetID();
				}
				break;
			case 2:
				{
				setState(318);
				datasetComplex();
				}
				break;
			case 3:
				{
				setState(319);
				match(T__12);
				setState(320);
				datasetID();
				setState(321);
				match(T__13);
				}
				break;
			case 4:
				{
				setState(323);
				match(T__12);
				setState(324);
				closedDataset(0);
				setState(325);
				match(T__13);
				}
				break;
			case 5:
				{
				setState(327);
				match(T__12);
				setState(328);
				membershipDataset();
				setState(329);
				match(T__13);
				}
				break;
			case 6:
				{
				setState(331);
				match(T__12);
				setState(332);
				openedDataset(0);
				setState(333);
				match(T__13);
				}
				break;
			case 7:
				{
				setState(335);
				((ClosedDatasetContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !(_la==T__0 || _la==T__1) ) {
					((ClosedDatasetContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(336);
				dataset();
				}
				break;
			case 8:
				{
				setState(337);
				((ClosedDatasetContext)_localctx).opSymbol = match(ROUND);
				setState(338);
				match(T__12);
				setState(339);
				dataset();
				setState(342);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==CARTESIAN_PER) {
					{
					setState(340);
					match(CARTESIAN_PER);
					setState(341);
					optionalExpr();
					}
				}

				setState(344);
				match(T__13);
				}
				break;
			case 9:
				{
				setState(346);
				((ClosedDatasetContext)_localctx).opSymbol = match(CEIL);
				setState(347);
				match(T__12);
				setState(348);
				dataset();
				setState(349);
				match(T__13);
				}
				break;
			case 10:
				{
				setState(351);
				((ClosedDatasetContext)_localctx).opSymbol = match(FLOOR);
				setState(352);
				match(T__12);
				setState(353);
				dataset();
				setState(354);
				match(T__13);
				}
				break;
			case 11:
				{
				setState(356);
				((ClosedDatasetContext)_localctx).opSymbol = match(ABS);
				setState(357);
				match(T__12);
				setState(358);
				dataset();
				setState(359);
				match(T__13);
				}
				break;
			case 12:
				{
				setState(361);
				((ClosedDatasetContext)_localctx).opSymbol = match(EXP);
				setState(362);
				match(T__12);
				setState(363);
				dataset();
				setState(364);
				match(T__13);
				}
				break;
			case 13:
				{
				setState(366);
				((ClosedDatasetContext)_localctx).opSymbol = match(LN);
				setState(367);
				match(T__12);
				setState(368);
				dataset();
				setState(369);
				match(T__13);
				}
				break;
			case 14:
				{
				setState(371);
				((ClosedDatasetContext)_localctx).opSymbol = match(LOG);
				setState(372);
				match(T__12);
				setState(373);
				dataset();
				setState(374);
				match(CARTESIAN_PER);
				setState(375);
				scalar(0);
				setState(376);
				match(T__13);
				}
				break;
			case 15:
				{
				setState(378);
				((ClosedDatasetContext)_localctx).opSymbol = match(TRUNC);
				setState(379);
				match(T__12);
				setState(380);
				dataset();
				setState(383);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==CARTESIAN_PER) {
					{
					setState(381);
					match(CARTESIAN_PER);
					setState(382);
					optionalExpr();
					}
				}

				setState(385);
				match(T__13);
				}
				break;
			case 16:
				{
				setState(387);
				((ClosedDatasetContext)_localctx).opSymbol = match(POWER);
				setState(388);
				match(T__12);
				setState(389);
				dataset();
				setState(390);
				match(CARTESIAN_PER);
				setState(391);
				scalar(0);
				setState(392);
				match(T__13);
				}
				break;
			case 17:
				{
				setState(394);
				((ClosedDatasetContext)_localctx).opSymbol = match(SQRT);
				setState(395);
				match(T__12);
				setState(396);
				dataset();
				setState(397);
				match(T__13);
				}
				break;
			case 18:
				{
				setState(399);
				((ClosedDatasetContext)_localctx).opSymbol = match(LEN);
				setState(400);
				match(T__12);
				setState(401);
				dataset();
				setState(402);
				match(T__13);
				}
				break;
			case 19:
				{
				setState(404);
				((ClosedDatasetContext)_localctx).opSymbol = match(BETWEEN);
				setState(405);
				match(T__12);
				setState(406);
				dataset();
				setState(407);
				match(CARTESIAN_PER);
				setState(408);
				scalar(0);
				setState(409);
				match(CARTESIAN_PER);
				setState(410);
				scalar(0);
				setState(411);
				match(T__13);
				}
				break;
			case 20:
				{
				setState(413);
				((ClosedDatasetContext)_localctx).opSymbol = match(TRIM);
				setState(414);
				match(T__12);
				setState(415);
				dataset();
				setState(416);
				match(T__13);
				}
				break;
			case 21:
				{
				setState(418);
				((ClosedDatasetContext)_localctx).opSymbol = match(LTRIM);
				setState(419);
				match(T__12);
				setState(420);
				dataset();
				setState(421);
				match(T__13);
				}
				break;
			case 22:
				{
				setState(423);
				((ClosedDatasetContext)_localctx).opSymbol = match(RTRIM);
				setState(424);
				match(T__12);
				setState(425);
				dataset();
				setState(426);
				match(T__13);
				}
				break;
			case 23:
				{
				setState(428);
				((ClosedDatasetContext)_localctx).opSymbol = match(UCASE);
				setState(429);
				match(T__12);
				setState(430);
				dataset();
				setState(431);
				match(T__13);
				}
				break;
			case 24:
				{
				setState(433);
				((ClosedDatasetContext)_localctx).opSymbol = match(LCASE);
				setState(434);
				match(T__12);
				setState(435);
				dataset();
				setState(436);
				match(T__13);
				}
				break;
			case 25:
				{
				setState(438);
				((ClosedDatasetContext)_localctx).opSymbol = match(SUBSTR);
				setState(439);
				match(T__12);
				setState(440);
				dataset();
				setState(443);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,33,_ctx) ) {
				case 1:
					{
					setState(441);
					match(CARTESIAN_PER);
					setState(442);
					optionalExpr();
					}
					break;
				}
				setState(447);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==CARTESIAN_PER) {
					{
					setState(445);
					match(CARTESIAN_PER);
					setState(446);
					optionalExpr();
					}
				}

				setState(449);
				match(T__13);
				}
				break;
			case 26:
				{
				setState(451);
				((ClosedDatasetContext)_localctx).opSymbol = match(INSTR);
				setState(452);
				match(T__12);
				setState(453);
				dataset();
				setState(454);
				match(CARTESIAN_PER);
				setState(455);
				scalar(0);
				setState(458);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,35,_ctx) ) {
				case 1:
					{
					setState(456);
					match(CARTESIAN_PER);
					setState(457);
					optionalExpr();
					}
					break;
				}
				setState(462);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==CARTESIAN_PER) {
					{
					setState(460);
					match(CARTESIAN_PER);
					setState(461);
					optionalExpr();
					}
				}

				setState(464);
				match(T__13);
				}
				break;
			case 27:
				{
				setState(466);
				((ClosedDatasetContext)_localctx).opSymbol = match(REPLACE);
				setState(467);
				match(T__12);
				setState(468);
				dataset();
				setState(469);
				match(CARTESIAN_PER);
				setState(470);
				scalar(0);
				setState(473);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==CARTESIAN_PER) {
					{
					setState(471);
					match(CARTESIAN_PER);
					setState(472);
					optionalExpr();
					}
				}

				setState(475);
				match(T__13);
				}
				break;
			case 28:
				{
				setState(477);
				((ClosedDatasetContext)_localctx).opSymbol = match(CHARSET_MATCH);
				setState(478);
				match(T__12);
				setState(479);
				dataset();
				setState(480);
				match(CARTESIAN_PER);
				setState(481);
				scalar(0);
				setState(482);
				match(T__13);
				}
				break;
			case 29:
				{
				setState(484);
				((ClosedDatasetContext)_localctx).opSymbol = match(ISNULL);
				setState(485);
				match(T__12);
				setState(486);
				dataset();
				setState(487);
				match(T__13);
				}
				break;
			case 30:
				{
				setState(489);
				((ClosedDatasetContext)_localctx).opSymbol = match(NVL);
				setState(490);
				match(T__12);
				setState(491);
				dataset();
				setState(492);
				match(CARTESIAN_PER);
				setState(495);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,38,_ctx) ) {
				case 1:
					{
					setState(493);
					dataset();
					}
					break;
				case 2:
					{
					setState(494);
					scalar(0);
					}
					break;
				}
				setState(497);
				match(T__13);
				}
				break;
			case 31:
				{
				setState(499);
				((ClosedDatasetContext)_localctx).opSymbol = match(MOD);
				setState(500);
				match(T__12);
				setState(501);
				dataset();
				setState(502);
				match(CARTESIAN_PER);
				setState(505);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,39,_ctx) ) {
				case 1:
					{
					setState(503);
					dataset();
					}
					break;
				case 2:
					{
					setState(504);
					scalar(0);
					}
					break;
				}
				setState(507);
				match(T__13);
				}
				break;
			case 32:
				{
				setState(509);
				((ClosedDatasetContext)_localctx).opSymbol = match(EXISTS_IN);
				setState(510);
				match(T__12);
				setState(511);
				dataset();
				setState(512);
				match(CARTESIAN_PER);
				setState(513);
				dataset();
				setState(516);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==CARTESIAN_PER) {
					{
					setState(514);
					match(CARTESIAN_PER);
					setState(515);
					retainType();
					}
				}

				setState(518);
				match(T__13);
				}
				break;
			case 33:
				{
				setState(520);
				((ClosedDatasetContext)_localctx).opSymbol = match(FLOW_TO_STOCK);
				setState(521);
				match(T__12);
				setState(522);
				dataset();
				setState(523);
				match(T__13);
				}
				break;
			case 34:
				{
				setState(525);
				((ClosedDatasetContext)_localctx).opSymbol = match(STOCK_TO_FLOW);
				setState(526);
				match(T__12);
				setState(527);
				dataset();
				setState(528);
				match(T__13);
				}
				break;
			case 35:
				{
				setState(530);
				((ClosedDatasetContext)_localctx).opSymbol = match(PERIOD_INDICATOR);
				setState(531);
				match(T__12);
				setState(532);
				dataset();
				setState(533);
				match(T__13);
				}
				break;
			case 36:
				{
				setState(535);
				((ClosedDatasetContext)_localctx).opSymbol = match(TIMESHIFT);
				setState(536);
				match(T__12);
				setState(537);
				dataset();
				setState(538);
				match(CARTESIAN_PER);
				setState(539);
				scalar(0);
				setState(540);
				match(T__13);
				}
				break;
			case 37:
				{
				setState(542);
				((ClosedDatasetContext)_localctx).opSymbol = match(FILL_TIME_SERIES);
				setState(543);
				match(T__12);
				setState(544);
				dataset();
				setState(547);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==CARTESIAN_PER) {
					{
					setState(545);
					match(CARTESIAN_PER);
					{
					setState(546);
					limitsMethod();
					}
					}
				}

				setState(549);
				match(T__13);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(560);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,43,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					{
					_localctx = new ClosedDatasetContext(_parentctx, _parentState);
					pushNewRecursionContext(_localctx, _startState, RULE_closedDataset);
					setState(553);
					if (!(precpred(_ctx, 36))) throw new FailedPredicateException(this, "precpred(_ctx, 36)");
					setState(554);
					match(T__10);
					setState(555);
					datasetClause();
					setState(556);
					match(T__11);
					}
					} 
				}
				setState(562);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,43,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class MembershipDatasetContext extends ParserRuleContext {
		public ClosedDatasetContext closedDataset() {
			return getRuleContext(ClosedDatasetContext.class,0);
		}
		public TerminalNode MEMBERSHIP() { return getToken(VtlParser.MEMBERSHIP, 0); }
		public ComponentIDContext componentID() {
			return getRuleContext(ComponentIDContext.class,0);
		}
		public MembershipDatasetContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_membershipDataset; }
	}

	public final MembershipDatasetContext membershipDataset() throws RecognitionException {
		MembershipDatasetContext _localctx = new MembershipDatasetContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_membershipDataset);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(563);
			closedDataset(0);
			setState(564);
			match(MEMBERSHIP);
			setState(565);
			componentID();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DatasetComplexContext extends ParserRuleContext {
		public AggrInvocationContext aggrInvocation() {
			return getRuleContext(AggrInvocationContext.class,0);
		}
		public AnalyticInvocationContext analyticInvocation() {
			return getRuleContext(AnalyticInvocationContext.class,0);
		}
		public SetExprContext setExpr() {
			return getRuleContext(SetExprContext.class,0);
		}
		public JoinExprContext joinExpr() {
			return getRuleContext(JoinExprContext.class,0);
		}
		public DatasetComplexContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_datasetComplex; }
	}

	public final DatasetComplexContext datasetComplex() throws RecognitionException {
		DatasetComplexContext _localctx = new DatasetComplexContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_datasetComplex);
		try {
			setState(571);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,44,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(567);
				aggrInvocation();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(568);
				analyticInvocation();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(569);
				setExpr();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(570);
				joinExpr();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IfThenElseDatasetContext extends ParserRuleContext {
		public DatasetContext ifDataset;
		public DatasetContext thenDataset;
		public ScalarContext thenScalar;
		public DatasetContext elseDataset;
		public ScalarContext elseScalar;
		public ScalarContext ifScalar;
		public TerminalNode IF() { return getToken(VtlParser.IF, 0); }
		public TerminalNode THEN() { return getToken(VtlParser.THEN, 0); }
		public TerminalNode ELSE() { return getToken(VtlParser.ELSE, 0); }
		public List<DatasetContext> dataset() {
			return getRuleContexts(DatasetContext.class);
		}
		public DatasetContext dataset(int i) {
			return getRuleContext(DatasetContext.class,i);
		}
		public List<ScalarContext> scalar() {
			return getRuleContexts(ScalarContext.class);
		}
		public ScalarContext scalar(int i) {
			return getRuleContext(ScalarContext.class,i);
		}
		public IfThenElseDatasetContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_ifThenElseDataset; }
	}

	public final IfThenElseDatasetContext ifThenElseDataset() throws RecognitionException {
		IfThenElseDatasetContext _localctx = new IfThenElseDatasetContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_ifThenElseDataset);
		try {
			setState(610);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,51,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(573);
				match(IF);
				setState(574);
				((IfThenElseDatasetContext)_localctx).ifDataset = dataset();
				setState(575);
				match(THEN);
				setState(578);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,45,_ctx) ) {
				case 1:
					{
					setState(576);
					((IfThenElseDatasetContext)_localctx).thenDataset = dataset();
					}
					break;
				case 2:
					{
					setState(577);
					((IfThenElseDatasetContext)_localctx).thenScalar = scalar(0);
					}
					break;
				}
				setState(580);
				match(ELSE);
				setState(583);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,46,_ctx) ) {
				case 1:
					{
					setState(581);
					((IfThenElseDatasetContext)_localctx).elseDataset = dataset();
					}
					break;
				case 2:
					{
					setState(582);
					((IfThenElseDatasetContext)_localctx).elseScalar = scalar(0);
					}
					break;
				}
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(585);
				match(IF);
				setState(588);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,47,_ctx) ) {
				case 1:
					{
					setState(586);
					((IfThenElseDatasetContext)_localctx).ifDataset = dataset();
					}
					break;
				case 2:
					{
					setState(587);
					((IfThenElseDatasetContext)_localctx).ifScalar = scalar(0);
					}
					break;
				}
				setState(590);
				match(THEN);
				setState(591);
				((IfThenElseDatasetContext)_localctx).thenDataset = dataset();
				setState(592);
				match(ELSE);
				setState(595);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,48,_ctx) ) {
				case 1:
					{
					setState(593);
					((IfThenElseDatasetContext)_localctx).elseDataset = dataset();
					}
					break;
				case 2:
					{
					setState(594);
					((IfThenElseDatasetContext)_localctx).elseScalar = scalar(0);
					}
					break;
				}
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(597);
				match(IF);
				setState(600);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,49,_ctx) ) {
				case 1:
					{
					setState(598);
					((IfThenElseDatasetContext)_localctx).ifDataset = dataset();
					}
					break;
				case 2:
					{
					setState(599);
					((IfThenElseDatasetContext)_localctx).ifScalar = scalar(0);
					}
					break;
				}
				setState(602);
				match(THEN);
				setState(605);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,50,_ctx) ) {
				case 1:
					{
					setState(603);
					((IfThenElseDatasetContext)_localctx).thenDataset = dataset();
					}
					break;
				case 2:
					{
					setState(604);
					((IfThenElseDatasetContext)_localctx).thenScalar = scalar(0);
					}
					break;
				}
				setState(607);
				match(ELSE);
				setState(608);
				((IfThenElseDatasetContext)_localctx).elseDataset = dataset();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class UnopenedDatasetContext extends ParserRuleContext {
		public ClosedDatasetContext closedDataset() {
			return getRuleContext(ClosedDatasetContext.class,0);
		}
		public MembershipDatasetContext membershipDataset() {
			return getRuleContext(MembershipDatasetContext.class,0);
		}
		public UnopenedDatasetContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_unopenedDataset; }
	}

	public final UnopenedDatasetContext unopenedDataset() throws RecognitionException {
		UnopenedDatasetContext _localctx = new UnopenedDatasetContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_unopenedDataset);
		try {
			setState(614);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,52,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(612);
				closedDataset(0);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(613);
				membershipDataset();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ComponentContext extends ParserRuleContext {
		public ComponentIDContext componentID() {
			return getRuleContext(ComponentIDContext.class,0);
		}
		public ClosedDatasetContext closedDataset() {
			return getRuleContext(ClosedDatasetContext.class,0);
		}
		public TerminalNode MEMBERSHIP() { return getToken(VtlParser.MEMBERSHIP, 0); }
		public ComponentContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_component; }
	}

	public final ComponentContext component() throws RecognitionException {
		ComponentContext _localctx = new ComponentContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_component);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(619);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,53,_ctx) ) {
			case 1:
				{
				setState(616);
				closedDataset(0);
				setState(617);
				match(MEMBERSHIP);
				}
				break;
			}
			setState(621);
			componentID();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ScalarContext extends ParserRuleContext {
		public Token opSymbol;
		public ConstantContext constant() {
			return getRuleContext(ConstantContext.class,0);
		}
		public ComponentContext component() {
			return getRuleContext(ComponentContext.class,0);
		}
		public List<ScalarContext> scalar() {
			return getRuleContexts(ScalarContext.class);
		}
		public ScalarContext scalar(int i) {
			return getRuleContext(ScalarContext.class,i);
		}
		public IfThenElseScalarContext ifThenElseScalar() {
			return getRuleContext(IfThenElseScalarContext.class,0);
		}
		public TerminalNode NOT() { return getToken(VtlParser.NOT, 0); }
		public TerminalNode ROUND() { return getToken(VtlParser.ROUND, 0); }
		public List<OptionalExprContext> optionalExpr() {
			return getRuleContexts(OptionalExprContext.class);
		}
		public OptionalExprContext optionalExpr(int i) {
			return getRuleContext(OptionalExprContext.class,i);
		}
		public TerminalNode CEIL() { return getToken(VtlParser.CEIL, 0); }
		public TerminalNode FLOOR() { return getToken(VtlParser.FLOOR, 0); }
		public TerminalNode ABS() { return getToken(VtlParser.ABS, 0); }
		public TerminalNode EXP() { return getToken(VtlParser.EXP, 0); }
		public TerminalNode LN() { return getToken(VtlParser.LN, 0); }
		public TerminalNode LOG() { return getToken(VtlParser.LOG, 0); }
		public TerminalNode TRUNC() { return getToken(VtlParser.TRUNC, 0); }
		public TerminalNode POWER() { return getToken(VtlParser.POWER, 0); }
		public TerminalNode SQRT() { return getToken(VtlParser.SQRT, 0); }
		public TerminalNode LEN() { return getToken(VtlParser.LEN, 0); }
		public TerminalNode BETWEEN() { return getToken(VtlParser.BETWEEN, 0); }
		public TerminalNode TRIM() { return getToken(VtlParser.TRIM, 0); }
		public TerminalNode LTRIM() { return getToken(VtlParser.LTRIM, 0); }
		public TerminalNode RTRIM() { return getToken(VtlParser.RTRIM, 0); }
		public TerminalNode UCASE() { return getToken(VtlParser.UCASE, 0); }
		public TerminalNode LCASE() { return getToken(VtlParser.LCASE, 0); }
		public TerminalNode SUBSTR() { return getToken(VtlParser.SUBSTR, 0); }
		public TerminalNode INSTR() { return getToken(VtlParser.INSTR, 0); }
		public TerminalNode REPLACE() { return getToken(VtlParser.REPLACE, 0); }
		public TerminalNode CHARSET_MATCH() { return getToken(VtlParser.CHARSET_MATCH, 0); }
		public TerminalNode ISNULL() { return getToken(VtlParser.ISNULL, 0); }
		public TerminalNode NVL() { return getToken(VtlParser.NVL, 0); }
		public TerminalNode MOD() { return getToken(VtlParser.MOD, 0); }
		public TerminalNode PERIOD_INDICATOR() { return getToken(VtlParser.PERIOD_INDICATOR, 0); }
		public TerminalNode CURRENT_DATE() { return getToken(VtlParser.CURRENT_DATE, 0); }
		public TerminalNode AND() { return getToken(VtlParser.AND, 0); }
		public TerminalNode OR() { return getToken(VtlParser.OR, 0); }
		public TerminalNode XOR() { return getToken(VtlParser.XOR, 0); }
		public TerminalNode CONCAT() { return getToken(VtlParser.CONCAT, 0); }
		public TerminalNode IN() { return getToken(VtlParser.IN, 0); }
		public TerminalNode NOT_IN() { return getToken(VtlParser.NOT_IN, 0); }
		public ListContext list() {
			return getRuleContext(ListContext.class,0);
		}
		public ValueDomainNameContext valueDomainName() {
			return getRuleContext(ValueDomainNameContext.class,0);
		}
		public ScalarContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_scalar; }
	}

	public final ScalarContext scalar() throws RecognitionException {
		return scalar(0);
	}

	private ScalarContext scalar(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ScalarContext _localctx = new ScalarContext(_ctx, _parentState);
		ScalarContext _prevctx = _localctx;
		int _startState = 20;
		enterRecursionRule(_localctx, 20, RULE_scalar, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(810);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,63,_ctx) ) {
			case 1:
				{
				setState(626);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case INTEGER_CONSTANT:
				case FLOAT_CONSTANT:
				case BOOLEAN_CONSTANT:
				case NULL_CONSTANT:
				case STRING_CONSTANT:
				case TIME_CONSTANT:
					{
					setState(624);
					constant();
					}
					break;
				case T__0:
				case T__1:
				case T__12:
				case BETWEEN:
				case ISNULL:
				case UNION:
				case SYMDIFF:
				case INTERSECT:
				case EXISTS_IN:
				case MIN:
				case MAX:
				case ABS:
				case LN:
				case LOG:
				case TRUNC:
				case ROUND:
				case POWER:
				case MOD:
				case LEN:
				case TRIM:
				case UCASE:
				case LCASE:
				case SUBSTR:
				case SUM:
				case AVG:
				case MEDIAN:
				case COUNT:
				case EXP:
				case CHARSET_MATCH:
				case NVL:
				case LTRIM:
				case RTRIM:
				case INSTR:
				case REPLACE:
				case CEIL:
				case FLOOR:
				case SQRT:
				case SETDIFF:
				case STDDEV_POP:
				case STDDEV_SAMP:
				case VAR_POP:
				case VAR_SAMP:
				case FIRST_VALUE:
				case LAST_VALUE:
				case LAG:
				case LEAD:
				case RATIO_TO_REPORT:
				case FILL_TIME_SERIES:
				case FLOW_TO_STOCK:
				case STOCK_TO_FLOW:
				case TIMESHIFT:
				case INNER_JOIN:
				case LEFT_JOIN:
				case CROSS_JOIN:
				case FULL_JOIN:
				case PERIOD_INDICATOR:
				case IDENTIFIER:
					{
					setState(625);
					component();
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				break;
			case 2:
				{
				setState(628);
				match(T__12);
				setState(629);
				scalar(0);
				setState(630);
				match(T__13);
				}
				break;
			case 3:
				{
				setState(632);
				ifThenElseScalar();
				}
				break;
			case 4:
				{
				setState(633);
				((ScalarContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !(_la==T__0 || _la==T__1) ) {
					((ScalarContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(634);
				component();
				}
				break;
			case 5:
				{
				setState(635);
				((ScalarContext)_localctx).opSymbol = match(NOT);
				setState(636);
				scalar(32);
				}
				break;
			case 6:
				{
				setState(637);
				((ScalarContext)_localctx).opSymbol = match(ROUND);
				setState(638);
				match(T__12);
				setState(639);
				scalar(0);
				setState(642);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==CARTESIAN_PER) {
					{
					setState(640);
					match(CARTESIAN_PER);
					setState(641);
					optionalExpr();
					}
				}

				setState(644);
				match(T__13);
				}
				break;
			case 7:
				{
				setState(646);
				((ScalarContext)_localctx).opSymbol = match(CEIL);
				setState(647);
				match(T__12);
				setState(648);
				scalar(0);
				setState(649);
				match(T__13);
				}
				break;
			case 8:
				{
				setState(651);
				((ScalarContext)_localctx).opSymbol = match(FLOOR);
				setState(652);
				match(T__12);
				setState(653);
				scalar(0);
				setState(654);
				match(T__13);
				}
				break;
			case 9:
				{
				setState(656);
				((ScalarContext)_localctx).opSymbol = match(ABS);
				setState(657);
				match(T__12);
				setState(658);
				scalar(0);
				setState(659);
				match(T__13);
				}
				break;
			case 10:
				{
				setState(661);
				((ScalarContext)_localctx).opSymbol = match(EXP);
				setState(662);
				match(T__12);
				setState(663);
				scalar(0);
				setState(664);
				match(T__13);
				}
				break;
			case 11:
				{
				setState(666);
				((ScalarContext)_localctx).opSymbol = match(LN);
				setState(667);
				match(T__12);
				setState(668);
				scalar(0);
				setState(669);
				match(T__13);
				}
				break;
			case 12:
				{
				setState(671);
				((ScalarContext)_localctx).opSymbol = match(LOG);
				setState(672);
				match(T__12);
				setState(673);
				scalar(0);
				setState(674);
				match(CARTESIAN_PER);
				setState(675);
				scalar(0);
				setState(676);
				match(T__13);
				}
				break;
			case 13:
				{
				setState(678);
				((ScalarContext)_localctx).opSymbol = match(TRUNC);
				setState(679);
				match(T__12);
				setState(680);
				scalar(0);
				setState(683);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==CARTESIAN_PER) {
					{
					setState(681);
					match(CARTESIAN_PER);
					setState(682);
					optionalExpr();
					}
				}

				setState(685);
				match(T__13);
				}
				break;
			case 14:
				{
				setState(687);
				((ScalarContext)_localctx).opSymbol = match(POWER);
				setState(688);
				match(T__12);
				setState(689);
				scalar(0);
				setState(690);
				match(CARTESIAN_PER);
				setState(691);
				scalar(0);
				setState(692);
				match(T__13);
				}
				break;
			case 15:
				{
				setState(694);
				((ScalarContext)_localctx).opSymbol = match(SQRT);
				setState(695);
				match(T__12);
				setState(696);
				scalar(0);
				setState(697);
				match(T__13);
				}
				break;
			case 16:
				{
				setState(699);
				((ScalarContext)_localctx).opSymbol = match(LEN);
				setState(700);
				match(T__12);
				setState(701);
				scalar(0);
				setState(702);
				match(T__13);
				}
				break;
			case 17:
				{
				setState(704);
				((ScalarContext)_localctx).opSymbol = match(BETWEEN);
				setState(705);
				match(T__12);
				setState(706);
				scalar(0);
				setState(707);
				match(CARTESIAN_PER);
				setState(708);
				scalar(0);
				setState(709);
				match(CARTESIAN_PER);
				setState(710);
				scalar(0);
				setState(711);
				match(T__13);
				}
				break;
			case 18:
				{
				setState(713);
				((ScalarContext)_localctx).opSymbol = match(TRIM);
				setState(714);
				match(T__12);
				setState(715);
				scalar(0);
				setState(716);
				match(T__13);
				}
				break;
			case 19:
				{
				setState(718);
				((ScalarContext)_localctx).opSymbol = match(LTRIM);
				setState(719);
				match(T__12);
				setState(720);
				scalar(0);
				setState(721);
				match(T__13);
				}
				break;
			case 20:
				{
				setState(723);
				((ScalarContext)_localctx).opSymbol = match(RTRIM);
				setState(724);
				match(T__12);
				setState(725);
				scalar(0);
				setState(726);
				match(T__13);
				}
				break;
			case 21:
				{
				setState(728);
				((ScalarContext)_localctx).opSymbol = match(UCASE);
				setState(729);
				match(T__12);
				setState(730);
				scalar(0);
				setState(731);
				match(T__13);
				}
				break;
			case 22:
				{
				setState(733);
				((ScalarContext)_localctx).opSymbol = match(LCASE);
				setState(734);
				match(T__12);
				setState(735);
				scalar(0);
				setState(736);
				match(T__13);
				}
				break;
			case 23:
				{
				setState(738);
				((ScalarContext)_localctx).opSymbol = match(SUBSTR);
				setState(739);
				match(T__12);
				setState(740);
				scalar(0);
				setState(743);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,57,_ctx) ) {
				case 1:
					{
					setState(741);
					match(CARTESIAN_PER);
					setState(742);
					optionalExpr();
					}
					break;
				}
				setState(747);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==CARTESIAN_PER) {
					{
					setState(745);
					match(CARTESIAN_PER);
					setState(746);
					optionalExpr();
					}
				}

				setState(749);
				match(T__13);
				}
				break;
			case 24:
				{
				setState(751);
				((ScalarContext)_localctx).opSymbol = match(INSTR);
				setState(752);
				match(T__12);
				setState(753);
				scalar(0);
				setState(754);
				match(CARTESIAN_PER);
				setState(755);
				scalar(0);
				setState(758);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,59,_ctx) ) {
				case 1:
					{
					setState(756);
					match(CARTESIAN_PER);
					setState(757);
					optionalExpr();
					}
					break;
				}
				setState(762);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==CARTESIAN_PER) {
					{
					setState(760);
					match(CARTESIAN_PER);
					setState(761);
					optionalExpr();
					}
				}

				setState(764);
				match(T__13);
				}
				break;
			case 25:
				{
				setState(766);
				((ScalarContext)_localctx).opSymbol = match(REPLACE);
				setState(767);
				match(T__12);
				setState(768);
				scalar(0);
				setState(769);
				match(CARTESIAN_PER);
				setState(770);
				scalar(0);
				setState(773);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==CARTESIAN_PER) {
					{
					setState(771);
					match(CARTESIAN_PER);
					setState(772);
					optionalExpr();
					}
				}

				setState(775);
				match(T__13);
				}
				break;
			case 26:
				{
				setState(777);
				((ScalarContext)_localctx).opSymbol = match(CHARSET_MATCH);
				setState(778);
				match(T__12);
				setState(779);
				scalar(0);
				setState(780);
				match(CARTESIAN_PER);
				setState(781);
				scalar(0);
				setState(782);
				match(T__13);
				}
				break;
			case 27:
				{
				setState(784);
				((ScalarContext)_localctx).opSymbol = match(ISNULL);
				setState(785);
				match(T__12);
				setState(786);
				scalar(0);
				setState(787);
				match(T__13);
				}
				break;
			case 28:
				{
				setState(789);
				((ScalarContext)_localctx).opSymbol = match(NVL);
				setState(790);
				match(T__12);
				setState(791);
				scalar(0);
				setState(792);
				match(CARTESIAN_PER);
				setState(793);
				scalar(0);
				setState(794);
				match(T__13);
				}
				break;
			case 29:
				{
				setState(796);
				((ScalarContext)_localctx).opSymbol = match(MOD);
				setState(797);
				match(T__12);
				setState(798);
				scalar(0);
				setState(799);
				match(CARTESIAN_PER);
				setState(800);
				scalar(0);
				setState(801);
				match(T__13);
				}
				break;
			case 30:
				{
				setState(803);
				((ScalarContext)_localctx).opSymbol = match(PERIOD_INDICATOR);
				setState(804);
				match(T__12);
				setState(806);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__12) | (1L << IF) | (1L << CURRENT_DATE) | (1L << NOT) | (1L << BETWEEN) | (1L << ISNULL) | (1L << UNION) | (1L << SYMDIFF) | (1L << INTERSECT) | (1L << EXISTS_IN))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (MIN - 66)) | (1L << (MAX - 66)) | (1L << (ABS - 66)) | (1L << (LN - 66)) | (1L << (LOG - 66)) | (1L << (TRUNC - 66)) | (1L << (ROUND - 66)) | (1L << (POWER - 66)) | (1L << (MOD - 66)) | (1L << (LEN - 66)) | (1L << (TRIM - 66)) | (1L << (UCASE - 66)) | (1L << (LCASE - 66)) | (1L << (SUBSTR - 66)) | (1L << (SUM - 66)) | (1L << (AVG - 66)) | (1L << (MEDIAN - 66)) | (1L << (COUNT - 66)) | (1L << (EXP - 66)) | (1L << (CHARSET_MATCH - 66)) | (1L << (NVL - 66)) | (1L << (LTRIM - 66)) | (1L << (RTRIM - 66)) | (1L << (INSTR - 66)) | (1L << (REPLACE - 66)) | (1L << (CEIL - 66)) | (1L << (FLOOR - 66)) | (1L << (SQRT - 66)) | (1L << (SETDIFF - 66)) | (1L << (STDDEV_POP - 66)) | (1L << (STDDEV_SAMP - 66)) | (1L << (VAR_POP - 66)) | (1L << (VAR_SAMP - 66)))) != 0) || ((((_la - 133)) & ~0x3f) == 0 && ((1L << (_la - 133)) & ((1L << (FIRST_VALUE - 133)) | (1L << (LAST_VALUE - 133)) | (1L << (LAG - 133)) | (1L << (LEAD - 133)) | (1L << (RATIO_TO_REPORT - 133)) | (1L << (FILL_TIME_SERIES - 133)) | (1L << (FLOW_TO_STOCK - 133)) | (1L << (STOCK_TO_FLOW - 133)) | (1L << (TIMESHIFT - 133)) | (1L << (INNER_JOIN - 133)) | (1L << (LEFT_JOIN - 133)) | (1L << (CROSS_JOIN - 133)) | (1L << (FULL_JOIN - 133)) | (1L << (PERIOD_INDICATOR - 133)))) != 0) || ((((_la - 225)) & ~0x3f) == 0 && ((1L << (_la - 225)) & ((1L << (INTEGER_CONSTANT - 225)) | (1L << (FLOAT_CONSTANT - 225)) | (1L << (BOOLEAN_CONSTANT - 225)) | (1L << (NULL_CONSTANT - 225)) | (1L << (STRING_CONSTANT - 225)) | (1L << (TIME_CONSTANT - 225)) | (1L << (IDENTIFIER - 225)))) != 0)) {
					{
					setState(805);
					scalar(0);
					}
				}

				setState(808);
				match(T__13);
				}
				break;
			case 31:
				{
				setState(809);
				((ScalarContext)_localctx).opSymbol = match(CURRENT_DATE);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(832);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,66,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(830);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,65,_ctx) ) {
					case 1:
						{
						_localctx = new ScalarContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_scalar);
						setState(812);
						if (!(precpred(_ctx, 31))) throw new FailedPredicateException(this, "precpred(_ctx, 31)");
						setState(813);
						((ScalarContext)_localctx).opSymbol = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==T__2 || _la==T__3) ) {
							((ScalarContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(814);
						scalar(32);
						}
						break;
					case 2:
						{
						_localctx = new ScalarContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_scalar);
						setState(815);
						if (!(precpred(_ctx, 30))) throw new FailedPredicateException(this, "precpred(_ctx, 30)");
						setState(816);
						((ScalarContext)_localctx).opSymbol = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==T__0 || _la==T__1) ) {
							((ScalarContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(817);
						scalar(31);
						}
						break;
					case 3:
						{
						_localctx = new ScalarContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_scalar);
						setState(818);
						if (!(precpred(_ctx, 29))) throw new FailedPredicateException(this, "precpred(_ctx, 29)");
						setState(819);
						((ScalarContext)_localctx).opSymbol = _input.LT(1);
						_la = _input.LA(1);
						if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__4) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__9))) != 0)) ) {
							((ScalarContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(820);
						scalar(30);
						}
						break;
					case 4:
						{
						_localctx = new ScalarContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_scalar);
						setState(821);
						if (!(precpred(_ctx, 28))) throw new FailedPredicateException(this, "precpred(_ctx, 28)");
						setState(822);
						((ScalarContext)_localctx).opSymbol = _input.LT(1);
						_la = _input.LA(1);
						if ( !(((((_la - 34)) & ~0x3f) == 0 && ((1L << (_la - 34)) & ((1L << (AND - 34)) | (1L << (OR - 34)) | (1L << (XOR - 34)) | (1L << (CONCAT - 34)))) != 0)) ) {
							((ScalarContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(823);
						scalar(29);
						}
						break;
					case 5:
						{
						_localctx = new ScalarContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_scalar);
						setState(824);
						if (!(precpred(_ctx, 27))) throw new FailedPredicateException(this, "precpred(_ctx, 27)");
						setState(825);
						((ScalarContext)_localctx).opSymbol = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==IN || _la==NOT_IN) ) {
							((ScalarContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(828);
						_errHandler.sync(this);
						switch (_input.LA(1)) {
						case T__14:
							{
							setState(826);
							list();
							}
							break;
						case IDENTIFIER:
							{
							setState(827);
							valueDomainName();
							}
							break;
						default:
							throw new NoViableAltException(this);
						}
						}
						break;
					}
					} 
				}
				setState(834);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,66,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class IfThenElseScalarContext extends ParserRuleContext {
		public TerminalNode IF() { return getToken(VtlParser.IF, 0); }
		public List<ScalarContext> scalar() {
			return getRuleContexts(ScalarContext.class);
		}
		public ScalarContext scalar(int i) {
			return getRuleContext(ScalarContext.class,i);
		}
		public TerminalNode THEN() { return getToken(VtlParser.THEN, 0); }
		public TerminalNode ELSE() { return getToken(VtlParser.ELSE, 0); }
		public IfThenElseScalarContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_ifThenElseScalar; }
	}

	public final IfThenElseScalarContext ifThenElseScalar() throws RecognitionException {
		IfThenElseScalarContext _localctx = new IfThenElseScalarContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_ifThenElseScalar);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(835);
			match(IF);
			setState(836);
			scalar(0);
			setState(837);
			match(THEN);
			setState(838);
			scalar(0);
			setState(839);
			match(ELSE);
			setState(840);
			scalar(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class OptionalExprContext extends ParserRuleContext {
		public ScalarContext scalar() {
			return getRuleContext(ScalarContext.class,0);
		}
		public TerminalNode OPTIONAL() { return getToken(VtlParser.OPTIONAL, 0); }
		public OptionalExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_optionalExpr; }
	}

	public final OptionalExprContext optionalExpr() throws RecognitionException {
		OptionalExprContext _localctx = new OptionalExprContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_optionalExpr);
		try {
			setState(844);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__0:
			case T__1:
			case T__12:
			case IF:
			case CURRENT_DATE:
			case NOT:
			case BETWEEN:
			case ISNULL:
			case UNION:
			case SYMDIFF:
			case INTERSECT:
			case EXISTS_IN:
			case MIN:
			case MAX:
			case ABS:
			case LN:
			case LOG:
			case TRUNC:
			case ROUND:
			case POWER:
			case MOD:
			case LEN:
			case TRIM:
			case UCASE:
			case LCASE:
			case SUBSTR:
			case SUM:
			case AVG:
			case MEDIAN:
			case COUNT:
			case EXP:
			case CHARSET_MATCH:
			case NVL:
			case LTRIM:
			case RTRIM:
			case INSTR:
			case REPLACE:
			case CEIL:
			case FLOOR:
			case SQRT:
			case SETDIFF:
			case STDDEV_POP:
			case STDDEV_SAMP:
			case VAR_POP:
			case VAR_SAMP:
			case FIRST_VALUE:
			case LAST_VALUE:
			case LAG:
			case LEAD:
			case RATIO_TO_REPORT:
			case FILL_TIME_SERIES:
			case FLOW_TO_STOCK:
			case STOCK_TO_FLOW:
			case TIMESHIFT:
			case INNER_JOIN:
			case LEFT_JOIN:
			case CROSS_JOIN:
			case FULL_JOIN:
			case PERIOD_INDICATOR:
			case INTEGER_CONSTANT:
			case FLOAT_CONSTANT:
			case BOOLEAN_CONSTANT:
			case NULL_CONSTANT:
			case STRING_CONSTANT:
			case TIME_CONSTANT:
			case IDENTIFIER:
				enterOuterAlt(_localctx, 1);
				{
				setState(842);
				scalar(0);
				}
				break;
			case OPTIONAL:
				enterOuterAlt(_localctx, 2);
				{
				setState(843);
				match(OPTIONAL);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class SetExprContext extends ParserRuleContext {
		public Token opSymbol;
		public List<DatasetContext> dataset() {
			return getRuleContexts(DatasetContext.class);
		}
		public DatasetContext dataset(int i) {
			return getRuleContext(DatasetContext.class,i);
		}
		public TerminalNode UNION() { return getToken(VtlParser.UNION, 0); }
		public TerminalNode SYMDIFF() { return getToken(VtlParser.SYMDIFF, 0); }
		public TerminalNode SETDIFF() { return getToken(VtlParser.SETDIFF, 0); }
		public TerminalNode INTERSECT() { return getToken(VtlParser.INTERSECT, 0); }
		public SetExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_setExpr; }
	}

	public final SetExprContext setExpr() throws RecognitionException {
		SetExprContext _localctx = new SetExprContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_setExpr);
		int _la;
		try {
			setState(884);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case UNION:
				enterOuterAlt(_localctx, 1);
				{
				setState(846);
				((SetExprContext)_localctx).opSymbol = match(UNION);
				setState(847);
				match(T__12);
				setState(848);
				dataset();
				setState(853);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==CARTESIAN_PER) {
					{
					{
					setState(849);
					match(CARTESIAN_PER);
					setState(850);
					dataset();
					}
					}
					setState(855);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(856);
				match(T__13);
				}
				break;
			case SYMDIFF:
				enterOuterAlt(_localctx, 2);
				{
				setState(858);
				((SetExprContext)_localctx).opSymbol = match(SYMDIFF);
				setState(859);
				match(T__12);
				setState(860);
				dataset();
				setState(861);
				match(CARTESIAN_PER);
				setState(862);
				dataset();
				setState(863);
				match(T__13);
				}
				break;
			case SETDIFF:
				enterOuterAlt(_localctx, 3);
				{
				setState(865);
				((SetExprContext)_localctx).opSymbol = match(SETDIFF);
				setState(866);
				match(T__12);
				setState(867);
				dataset();
				setState(868);
				match(CARTESIAN_PER);
				setState(869);
				dataset();
				setState(870);
				match(T__13);
				}
				break;
			case INTERSECT:
				enterOuterAlt(_localctx, 4);
				{
				setState(872);
				((SetExprContext)_localctx).opSymbol = match(INTERSECT);
				setState(873);
				match(T__12);
				setState(874);
				dataset();
				setState(879);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==CARTESIAN_PER) {
					{
					{
					setState(875);
					match(CARTESIAN_PER);
					setState(876);
					dataset();
					}
					}
					setState(881);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(882);
				match(T__13);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DatasetClauseContext extends ParserRuleContext {
		public AggrClauseContext aggrClause() {
			return getRuleContext(AggrClauseContext.class,0);
		}
		public AnalyticClauseContext analyticClause() {
			return getRuleContext(AnalyticClauseContext.class,0);
		}
		public FilterClauseContext filterClause() {
			return getRuleContext(FilterClauseContext.class,0);
		}
		public RenameClauseContext renameClause() {
			return getRuleContext(RenameClauseContext.class,0);
		}
		public CalcClauseContext calcClause() {
			return getRuleContext(CalcClauseContext.class,0);
		}
		public KeepClauseContext keepClause() {
			return getRuleContext(KeepClauseContext.class,0);
		}
		public DropClauseContext dropClause() {
			return getRuleContext(DropClauseContext.class,0);
		}
		public PivotClauseContext pivotClause() {
			return getRuleContext(PivotClauseContext.class,0);
		}
		public UnpivotClauseContext unpivotClause() {
			return getRuleContext(UnpivotClauseContext.class,0);
		}
		public SubspaceClauseContext subspaceClause() {
			return getRuleContext(SubspaceClauseContext.class,0);
		}
		public DatasetClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_datasetClause; }
	}

	public final DatasetClauseContext datasetClause() throws RecognitionException {
		DatasetClauseContext _localctx = new DatasetClauseContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_datasetClause);
		try {
			setState(896);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case AGGREGATE:
				enterOuterAlt(_localctx, 1);
				{
				setState(886);
				aggrClause();
				}
				break;
			case ORDER:
			case DATA:
			case PARTITION:
			case RANGE:
				enterOuterAlt(_localctx, 2);
				{
				setState(887);
				analyticClause();
				}
				break;
			case FILTER:
				enterOuterAlt(_localctx, 3);
				{
				setState(888);
				filterClause();
				}
				break;
			case RENAME:
				enterOuterAlt(_localctx, 4);
				{
				setState(889);
				renameClause();
				}
				break;
			case CALC:
				enterOuterAlt(_localctx, 5);
				{
				setState(890);
				calcClause();
				}
				break;
			case KEEP:
				enterOuterAlt(_localctx, 6);
				{
				setState(891);
				keepClause();
				}
				break;
			case DROP:
				enterOuterAlt(_localctx, 7);
				{
				setState(892);
				dropClause();
				}
				break;
			case PIVOT:
				enterOuterAlt(_localctx, 8);
				{
				setState(893);
				pivotClause();
				}
				break;
			case UNPIVOT:
				enterOuterAlt(_localctx, 9);
				{
				setState(894);
				unpivotClause();
				}
				break;
			case SUBSPACE:
				enterOuterAlt(_localctx, 10);
				{
				setState(895);
				subspaceClause();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AggrClauseContext extends ParserRuleContext {
		public TerminalNode AGGREGATE() { return getToken(VtlParser.AGGREGATE, 0); }
		public List<AggrExprContext> aggrExpr() {
			return getRuleContexts(AggrExprContext.class);
		}
		public AggrExprContext aggrExpr(int i) {
			return getRuleContext(AggrExprContext.class,i);
		}
		public GroupingClauseContext groupingClause() {
			return getRuleContext(GroupingClauseContext.class,0);
		}
		public HavingClauseContext havingClause() {
			return getRuleContext(HavingClauseContext.class,0);
		}
		public AggrClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_aggrClause; }
	}

	public final AggrClauseContext aggrClause() throws RecognitionException {
		AggrClauseContext _localctx = new AggrClauseContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_aggrClause);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(898);
			match(AGGREGATE);
			setState(899);
			aggrExpr();
			setState(904);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==CARTESIAN_PER) {
				{
				{
				setState(900);
				match(CARTESIAN_PER);
				setState(901);
				aggrExpr();
				}
				}
				setState(906);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(908);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,73,_ctx) ) {
			case 1:
				{
				setState(907);
				groupingClause();
				}
				break;
			}
			setState(911);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==HAVING) {
				{
				setState(910);
				havingClause();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AggrExprContext extends ParserRuleContext {
		public ComponentIDContext componentID() {
			return getRuleContext(ComponentIDContext.class,0);
		}
		public AggrFunctionContext aggrFunction() {
			return getRuleContext(AggrFunctionContext.class,0);
		}
		public ComponentRoleContext componentRole() {
			return getRuleContext(ComponentRoleContext.class,0);
		}
		public AggrExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_aggrExpr; }
	}

	public final AggrExprContext aggrExpr() throws RecognitionException {
		AggrExprContext _localctx = new AggrExprContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_aggrExpr);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(914);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (((((_la - 89)) & ~0x3f) == 0 && ((1L << (_la - 89)) & ((1L << (DIMENSION - 89)) | (1L << (MEASURE - 89)) | (1L << (ATTRIBUTE - 89)) | (1L << (VIRAL - 89)))) != 0)) {
				{
				setState(913);
				componentRole();
				}
			}

			setState(916);
			componentID();
			setState(917);
			match(ASSIGN);
			setState(918);
			aggrFunction();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FilterClauseContext extends ParserRuleContext {
		public TerminalNode FILTER() { return getToken(VtlParser.FILTER, 0); }
		public ScalarContext scalar() {
			return getRuleContext(ScalarContext.class,0);
		}
		public FilterClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_filterClause; }
	}

	public final FilterClauseContext filterClause() throws RecognitionException {
		FilterClauseContext _localctx = new FilterClauseContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_filterClause);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(920);
			match(FILTER);
			setState(921);
			scalar(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class RenameClauseContext extends ParserRuleContext {
		public TerminalNode RENAME() { return getToken(VtlParser.RENAME, 0); }
		public List<RenameExprContext> renameExpr() {
			return getRuleContexts(RenameExprContext.class);
		}
		public RenameExprContext renameExpr(int i) {
			return getRuleContext(RenameExprContext.class,i);
		}
		public RenameClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_renameClause; }
	}

	public final RenameClauseContext renameClause() throws RecognitionException {
		RenameClauseContext _localctx = new RenameClauseContext(_ctx, getState());
		enterRule(_localctx, 36, RULE_renameClause);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(923);
			match(RENAME);
			setState(924);
			renameExpr();
			setState(929);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==CARTESIAN_PER) {
				{
				{
				setState(925);
				match(CARTESIAN_PER);
				setState(926);
				renameExpr();
				}
				}
				setState(931);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class RenameExprContext extends ParserRuleContext {
		public ComponentContext component() {
			return getRuleContext(ComponentContext.class,0);
		}
		public TerminalNode TO() { return getToken(VtlParser.TO, 0); }
		public ComponentIDContext componentID() {
			return getRuleContext(ComponentIDContext.class,0);
		}
		public RenameExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_renameExpr; }
	}

	public final RenameExprContext renameExpr() throws RecognitionException {
		RenameExprContext _localctx = new RenameExprContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_renameExpr);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(932);
			component();
			setState(933);
			match(TO);
			setState(934);
			componentID();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class CalcClauseContext extends ParserRuleContext {
		public TerminalNode CALC() { return getToken(VtlParser.CALC, 0); }
		public List<CalcExprContext> calcExpr() {
			return getRuleContexts(CalcExprContext.class);
		}
		public CalcExprContext calcExpr(int i) {
			return getRuleContext(CalcExprContext.class,i);
		}
		public CalcClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_calcClause; }
	}

	public final CalcClauseContext calcClause() throws RecognitionException {
		CalcClauseContext _localctx = new CalcClauseContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_calcClause);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(936);
			match(CALC);
			setState(937);
			calcExpr();
			setState(942);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==CARTESIAN_PER) {
				{
				{
				setState(938);
				match(CARTESIAN_PER);
				setState(939);
				calcExpr();
				}
				}
				setState(944);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class CalcExprContext extends ParserRuleContext {
		public ComponentIDContext componentID() {
			return getRuleContext(ComponentIDContext.class,0);
		}
		public ScalarContext scalar() {
			return getRuleContext(ScalarContext.class,0);
		}
		public AnalyticFunctionContext analyticFunction() {
			return getRuleContext(AnalyticFunctionContext.class,0);
		}
		public ComponentRoleContext componentRole() {
			return getRuleContext(ComponentRoleContext.class,0);
		}
		public CalcExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_calcExpr; }
	}

	public final CalcExprContext calcExpr() throws RecognitionException {
		CalcExprContext _localctx = new CalcExprContext(_ctx, getState());
		enterRule(_localctx, 42, RULE_calcExpr);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(946);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (((((_la - 89)) & ~0x3f) == 0 && ((1L << (_la - 89)) & ((1L << (DIMENSION - 89)) | (1L << (MEASURE - 89)) | (1L << (ATTRIBUTE - 89)) | (1L << (VIRAL - 89)))) != 0)) {
				{
				setState(945);
				componentRole();
				}
			}

			setState(948);
			componentID();
			setState(949);
			match(ASSIGN);
			setState(952);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,79,_ctx) ) {
			case 1:
				{
				setState(950);
				scalar(0);
				}
				break;
			case 2:
				{
				setState(951);
				analyticFunction();
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class KeepClauseContext extends ParserRuleContext {
		public TerminalNode KEEP() { return getToken(VtlParser.KEEP, 0); }
		public List<ComponentContext> component() {
			return getRuleContexts(ComponentContext.class);
		}
		public ComponentContext component(int i) {
			return getRuleContext(ComponentContext.class,i);
		}
		public KeepClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_keepClause; }
	}

	public final KeepClauseContext keepClause() throws RecognitionException {
		KeepClauseContext _localctx = new KeepClauseContext(_ctx, getState());
		enterRule(_localctx, 44, RULE_keepClause);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(954);
			match(KEEP);
			setState(955);
			component();
			setState(960);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==CARTESIAN_PER) {
				{
				{
				setState(956);
				match(CARTESIAN_PER);
				setState(957);
				component();
				}
				}
				setState(962);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DropClauseContext extends ParserRuleContext {
		public TerminalNode DROP() { return getToken(VtlParser.DROP, 0); }
		public List<ComponentContext> component() {
			return getRuleContexts(ComponentContext.class);
		}
		public ComponentContext component(int i) {
			return getRuleContext(ComponentContext.class,i);
		}
		public DropClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_dropClause; }
	}

	public final DropClauseContext dropClause() throws RecognitionException {
		DropClauseContext _localctx = new DropClauseContext(_ctx, getState());
		enterRule(_localctx, 46, RULE_dropClause);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(963);
			match(DROP);
			setState(964);
			component();
			setState(969);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==CARTESIAN_PER) {
				{
				{
				setState(965);
				match(CARTESIAN_PER);
				setState(966);
				component();
				}
				}
				setState(971);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PivotClauseContext extends ParserRuleContext {
		public TerminalNode PIVOT() { return getToken(VtlParser.PIVOT, 0); }
		public List<ComponentIDContext> componentID() {
			return getRuleContexts(ComponentIDContext.class);
		}
		public ComponentIDContext componentID(int i) {
			return getRuleContext(ComponentIDContext.class,i);
		}
		public PivotClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_pivotClause; }
	}

	public final PivotClauseContext pivotClause() throws RecognitionException {
		PivotClauseContext _localctx = new PivotClauseContext(_ctx, getState());
		enterRule(_localctx, 48, RULE_pivotClause);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(972);
			match(PIVOT);
			setState(973);
			componentID();
			setState(974);
			match(CARTESIAN_PER);
			setState(975);
			componentID();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class UnpivotClauseContext extends ParserRuleContext {
		public TerminalNode UNPIVOT() { return getToken(VtlParser.UNPIVOT, 0); }
		public List<ComponentIDContext> componentID() {
			return getRuleContexts(ComponentIDContext.class);
		}
		public ComponentIDContext componentID(int i) {
			return getRuleContext(ComponentIDContext.class,i);
		}
		public UnpivotClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_unpivotClause; }
	}

	public final UnpivotClauseContext unpivotClause() throws RecognitionException {
		UnpivotClauseContext _localctx = new UnpivotClauseContext(_ctx, getState());
		enterRule(_localctx, 50, RULE_unpivotClause);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(977);
			match(UNPIVOT);
			setState(978);
			componentID();
			setState(979);
			match(CARTESIAN_PER);
			setState(980);
			componentID();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class SubspaceClauseContext extends ParserRuleContext {
		public TerminalNode SUBSPACE() { return getToken(VtlParser.SUBSPACE, 0); }
		public List<SubspaceExprContext> subspaceExpr() {
			return getRuleContexts(SubspaceExprContext.class);
		}
		public SubspaceExprContext subspaceExpr(int i) {
			return getRuleContext(SubspaceExprContext.class,i);
		}
		public SubspaceClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_subspaceClause; }
	}

	public final SubspaceClauseContext subspaceClause() throws RecognitionException {
		SubspaceClauseContext _localctx = new SubspaceClauseContext(_ctx, getState());
		enterRule(_localctx, 52, RULE_subspaceClause);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(982);
			match(SUBSPACE);
			setState(983);
			subspaceExpr();
			setState(988);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==CARTESIAN_PER) {
				{
				{
				setState(984);
				match(CARTESIAN_PER);
				setState(985);
				subspaceExpr();
				}
				}
				setState(990);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class SubspaceExprContext extends ParserRuleContext {
		public ComponentContext component() {
			return getRuleContext(ComponentContext.class,0);
		}
		public ConstantContext constant() {
			return getRuleContext(ConstantContext.class,0);
		}
		public SubspaceExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_subspaceExpr; }
	}

	public final SubspaceExprContext subspaceExpr() throws RecognitionException {
		SubspaceExprContext _localctx = new SubspaceExprContext(_ctx, getState());
		enterRule(_localctx, 54, RULE_subspaceExpr);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(991);
			component();
			setState(992);
			match(T__8);
			setState(993);
			constant();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JoinExprContext extends ParserRuleContext {
		public JoinKeywordContext joinKeyword() {
			return getRuleContext(JoinKeywordContext.class,0);
		}
		public JoinClauseContext joinClause() {
			return getRuleContext(JoinClauseContext.class,0);
		}
		public JoinBodyContext joinBody() {
			return getRuleContext(JoinBodyContext.class,0);
		}
		public JoinExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_joinExpr; }
	}

	public final JoinExprContext joinExpr() throws RecognitionException {
		JoinExprContext _localctx = new JoinExprContext(_ctx, getState());
		enterRule(_localctx, 56, RULE_joinExpr);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(995);
			joinKeyword();
			setState(996);
			match(T__12);
			setState(997);
			joinClause();
			setState(999);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,83,_ctx) ) {
			case 1:
				{
				setState(998);
				joinBody();
				}
				break;
			}
			setState(1001);
			match(T__13);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JoinClauseContext extends ParserRuleContext {
		public JoinAliasesClauseContext joinAliasesClause() {
			return getRuleContext(JoinAliasesClauseContext.class,0);
		}
		public JoinUsingClauseContext joinUsingClause() {
			return getRuleContext(JoinUsingClauseContext.class,0);
		}
		public JoinClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_joinClause; }
	}

	public final JoinClauseContext joinClause() throws RecognitionException {
		JoinClauseContext _localctx = new JoinClauseContext(_ctx, getState());
		enterRule(_localctx, 58, RULE_joinClause);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1003);
			joinAliasesClause();
			setState(1005);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==USING) {
				{
				setState(1004);
				joinUsingClause();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JoinBodyContext extends ParserRuleContext {
		public JoinFilterClauseContext joinFilterClause() {
			return getRuleContext(JoinFilterClauseContext.class,0);
		}
		public JoinCalcClauseContext joinCalcClause() {
			return getRuleContext(JoinCalcClauseContext.class,0);
		}
		public JoinApplyClauseContext joinApplyClause() {
			return getRuleContext(JoinApplyClauseContext.class,0);
		}
		public JoinKeepClauseContext joinKeepClause() {
			return getRuleContext(JoinKeepClauseContext.class,0);
		}
		public JoinDropClauseContext joinDropClause() {
			return getRuleContext(JoinDropClauseContext.class,0);
		}
		public JoinRenameClauseContext joinRenameClause() {
			return getRuleContext(JoinRenameClauseContext.class,0);
		}
		public JoinAggrClauseContext joinAggrClause() {
			return getRuleContext(JoinAggrClauseContext.class,0);
		}
		public GroupingClauseContext groupingClause() {
			return getRuleContext(GroupingClauseContext.class,0);
		}
		public HavingClauseContext havingClause() {
			return getRuleContext(HavingClauseContext.class,0);
		}
		public JoinBodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_joinBody; }
	}

	public final JoinBodyContext joinBody() throws RecognitionException {
		JoinBodyContext _localctx = new JoinBodyContext(_ctx, getState());
		enterRule(_localctx, 60, RULE_joinBody);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1008);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==FILTER) {
				{
				setState(1007);
				joinFilterClause();
				}
			}

			setState(1017);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case CALC:
				{
				setState(1010);
				joinCalcClause();
				}
				break;
			case APPLY:
				{
				setState(1011);
				joinApplyClause();
				}
				break;
			case AGGREGATE:
				{
				{
				setState(1012);
				joinAggrClause();
				setState(1013);
				groupingClause();
				setState(1015);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==HAVING) {
					{
					setState(1014);
					havingClause();
					}
				}

				}
				}
				break;
			case T__13:
			case DROP:
			case KEEP:
			case RENAME:
				break;
			default:
				break;
			}
			setState(1021);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case KEEP:
				{
				setState(1019);
				joinKeepClause();
				}
				break;
			case DROP:
				{
				setState(1020);
				joinDropClause();
				}
				break;
			case T__13:
			case RENAME:
				break;
			default:
				break;
			}
			setState(1024);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==RENAME) {
				{
				setState(1023);
				joinRenameClause();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JoinAliasesClauseContext extends ParserRuleContext {
		public List<JoinAliasExprContext> joinAliasExpr() {
			return getRuleContexts(JoinAliasExprContext.class);
		}
		public JoinAliasExprContext joinAliasExpr(int i) {
			return getRuleContext(JoinAliasExprContext.class,i);
		}
		public JoinAliasesClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_joinAliasesClause; }
	}

	public final JoinAliasesClauseContext joinAliasesClause() throws RecognitionException {
		JoinAliasesClauseContext _localctx = new JoinAliasesClauseContext(_ctx, getState());
		enterRule(_localctx, 62, RULE_joinAliasesClause);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1026);
			joinAliasExpr();
			setState(1031);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==CARTESIAN_PER) {
				{
				{
				setState(1027);
				match(CARTESIAN_PER);
				setState(1028);
				joinAliasExpr();
				}
				}
				setState(1033);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JoinAliasExprContext extends ParserRuleContext {
		public DatasetContext dataset() {
			return getRuleContext(DatasetContext.class,0);
		}
		public TerminalNode AS() { return getToken(VtlParser.AS, 0); }
		public VarIDContext varID() {
			return getRuleContext(VarIDContext.class,0);
		}
		public JoinAliasExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_joinAliasExpr; }
	}

	public final JoinAliasExprContext joinAliasExpr() throws RecognitionException {
		JoinAliasExprContext _localctx = new JoinAliasExprContext(_ctx, getState());
		enterRule(_localctx, 64, RULE_joinAliasExpr);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1034);
			dataset();
			setState(1037);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==AS) {
				{
				setState(1035);
				match(AS);
				setState(1036);
				varID();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JoinUsingClauseContext extends ParserRuleContext {
		public TerminalNode USING() { return getToken(VtlParser.USING, 0); }
		public List<ComponentIDContext> componentID() {
			return getRuleContexts(ComponentIDContext.class);
		}
		public ComponentIDContext componentID(int i) {
			return getRuleContext(ComponentIDContext.class,i);
		}
		public JoinUsingClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_joinUsingClause; }
	}

	public final JoinUsingClauseContext joinUsingClause() throws RecognitionException {
		JoinUsingClauseContext _localctx = new JoinUsingClauseContext(_ctx, getState());
		enterRule(_localctx, 66, RULE_joinUsingClause);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1039);
			match(USING);
			setState(1040);
			componentID();
			setState(1045);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==CARTESIAN_PER) {
				{
				{
				setState(1041);
				match(CARTESIAN_PER);
				setState(1042);
				componentID();
				}
				}
				setState(1047);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JoinCalcClauseContext extends ParserRuleContext {
		public CalcClauseContext calcClause() {
			return getRuleContext(CalcClauseContext.class,0);
		}
		public JoinCalcClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_joinCalcClause; }
	}

	public final JoinCalcClauseContext joinCalcClause() throws RecognitionException {
		JoinCalcClauseContext _localctx = new JoinCalcClauseContext(_ctx, getState());
		enterRule(_localctx, 68, RULE_joinCalcClause);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1048);
			calcClause();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JoinAggrClauseContext extends ParserRuleContext {
		public AggrClauseContext aggrClause() {
			return getRuleContext(AggrClauseContext.class,0);
		}
		public JoinAggrClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_joinAggrClause; }
	}

	public final JoinAggrClauseContext joinAggrClause() throws RecognitionException {
		JoinAggrClauseContext _localctx = new JoinAggrClauseContext(_ctx, getState());
		enterRule(_localctx, 70, RULE_joinAggrClause);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1050);
			aggrClause();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JoinKeepClauseContext extends ParserRuleContext {
		public KeepClauseContext keepClause() {
			return getRuleContext(KeepClauseContext.class,0);
		}
		public JoinKeepClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_joinKeepClause; }
	}

	public final JoinKeepClauseContext joinKeepClause() throws RecognitionException {
		JoinKeepClauseContext _localctx = new JoinKeepClauseContext(_ctx, getState());
		enterRule(_localctx, 72, RULE_joinKeepClause);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1052);
			keepClause();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JoinDropClauseContext extends ParserRuleContext {
		public DropClauseContext dropClause() {
			return getRuleContext(DropClauseContext.class,0);
		}
		public JoinDropClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_joinDropClause; }
	}

	public final JoinDropClauseContext joinDropClause() throws RecognitionException {
		JoinDropClauseContext _localctx = new JoinDropClauseContext(_ctx, getState());
		enterRule(_localctx, 74, RULE_joinDropClause);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1054);
			dropClause();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JoinFilterClauseContext extends ParserRuleContext {
		public FilterClauseContext filterClause() {
			return getRuleContext(FilterClauseContext.class,0);
		}
		public JoinFilterClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_joinFilterClause; }
	}

	public final JoinFilterClauseContext joinFilterClause() throws RecognitionException {
		JoinFilterClauseContext _localctx = new JoinFilterClauseContext(_ctx, getState());
		enterRule(_localctx, 76, RULE_joinFilterClause);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1056);
			filterClause();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JoinRenameClauseContext extends ParserRuleContext {
		public RenameClauseContext renameClause() {
			return getRuleContext(RenameClauseContext.class,0);
		}
		public JoinRenameClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_joinRenameClause; }
	}

	public final JoinRenameClauseContext joinRenameClause() throws RecognitionException {
		JoinRenameClauseContext _localctx = new JoinRenameClauseContext(_ctx, getState());
		enterRule(_localctx, 78, RULE_joinRenameClause);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1058);
			renameClause();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JoinApplyClauseContext extends ParserRuleContext {
		public TerminalNode APPLY() { return getToken(VtlParser.APPLY, 0); }
		public ScalarContext scalar() {
			return getRuleContext(ScalarContext.class,0);
		}
		public JoinApplyClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_joinApplyClause; }
	}

	public final JoinApplyClauseContext joinApplyClause() throws RecognitionException {
		JoinApplyClauseContext _localctx = new JoinApplyClauseContext(_ctx, getState());
		enterRule(_localctx, 80, RULE_joinApplyClause);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1060);
			match(APPLY);
			setState(1061);
			scalar(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AggrInvocationContext extends ParserRuleContext {
		public AggrFunctionNameContext opSymbol;
		public DatasetContext dataset() {
			return getRuleContext(DatasetContext.class,0);
		}
		public GroupingClauseContext groupingClause() {
			return getRuleContext(GroupingClauseContext.class,0);
		}
		public AggrFunctionNameContext aggrFunctionName() {
			return getRuleContext(AggrFunctionNameContext.class,0);
		}
		public HavingClauseContext havingClause() {
			return getRuleContext(HavingClauseContext.class,0);
		}
		public AggrInvocationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_aggrInvocation; }
	}

	public final AggrInvocationContext aggrInvocation() throws RecognitionException {
		AggrInvocationContext _localctx = new AggrInvocationContext(_ctx, getState());
		enterRule(_localctx, 82, RULE_aggrInvocation);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1063);
			((AggrInvocationContext)_localctx).opSymbol = aggrFunctionName();
			setState(1064);
			match(T__12);
			setState(1065);
			dataset();
			setState(1066);
			groupingClause();
			setState(1068);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==HAVING) {
				{
				setState(1067);
				havingClause();
				}
			}

			setState(1070);
			match(T__13);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AggrFunctionContext extends ParserRuleContext {
		public Token opSymbol;
		public ComponentContext component() {
			return getRuleContext(ComponentContext.class,0);
		}
		public TerminalNode SUM() { return getToken(VtlParser.SUM, 0); }
		public TerminalNode AVG() { return getToken(VtlParser.AVG, 0); }
		public TerminalNode COUNT() { return getToken(VtlParser.COUNT, 0); }
		public TerminalNode MEDIAN() { return getToken(VtlParser.MEDIAN, 0); }
		public TerminalNode MIN() { return getToken(VtlParser.MIN, 0); }
		public TerminalNode MAX() { return getToken(VtlParser.MAX, 0); }
		public TerminalNode RANK() { return getToken(VtlParser.RANK, 0); }
		public TerminalNode STDDEV_POP() { return getToken(VtlParser.STDDEV_POP, 0); }
		public TerminalNode STDDEV_SAMP() { return getToken(VtlParser.STDDEV_SAMP, 0); }
		public TerminalNode VAR_POP() { return getToken(VtlParser.VAR_POP, 0); }
		public TerminalNode VAR_SAMP() { return getToken(VtlParser.VAR_SAMP, 0); }
		public AggrFunctionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_aggrFunction; }
	}

	public final AggrFunctionContext aggrFunction() throws RecognitionException {
		AggrFunctionContext _localctx = new AggrFunctionContext(_ctx, getState());
		enterRule(_localctx, 84, RULE_aggrFunction);
		int _la;
		try {
			setState(1128);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case SUM:
				enterOuterAlt(_localctx, 1);
				{
				setState(1072);
				((AggrFunctionContext)_localctx).opSymbol = match(SUM);
				setState(1073);
				match(T__12);
				setState(1074);
				component();
				setState(1075);
				match(T__13);
				}
				break;
			case AVG:
				enterOuterAlt(_localctx, 2);
				{
				setState(1077);
				((AggrFunctionContext)_localctx).opSymbol = match(AVG);
				setState(1078);
				match(T__12);
				setState(1079);
				component();
				setState(1080);
				match(T__13);
				}
				break;
			case COUNT:
				enterOuterAlt(_localctx, 3);
				{
				setState(1082);
				((AggrFunctionContext)_localctx).opSymbol = match(COUNT);
				setState(1083);
				match(T__12);
				setState(1085);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__12) | (1L << BETWEEN) | (1L << ISNULL) | (1L << UNION) | (1L << SYMDIFF) | (1L << INTERSECT) | (1L << EXISTS_IN))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (MIN - 66)) | (1L << (MAX - 66)) | (1L << (ABS - 66)) | (1L << (LN - 66)) | (1L << (LOG - 66)) | (1L << (TRUNC - 66)) | (1L << (ROUND - 66)) | (1L << (POWER - 66)) | (1L << (MOD - 66)) | (1L << (LEN - 66)) | (1L << (TRIM - 66)) | (1L << (UCASE - 66)) | (1L << (LCASE - 66)) | (1L << (SUBSTR - 66)) | (1L << (SUM - 66)) | (1L << (AVG - 66)) | (1L << (MEDIAN - 66)) | (1L << (COUNT - 66)) | (1L << (EXP - 66)) | (1L << (CHARSET_MATCH - 66)) | (1L << (NVL - 66)) | (1L << (LTRIM - 66)) | (1L << (RTRIM - 66)) | (1L << (INSTR - 66)) | (1L << (REPLACE - 66)) | (1L << (CEIL - 66)) | (1L << (FLOOR - 66)) | (1L << (SQRT - 66)) | (1L << (SETDIFF - 66)) | (1L << (STDDEV_POP - 66)) | (1L << (STDDEV_SAMP - 66)) | (1L << (VAR_POP - 66)) | (1L << (VAR_SAMP - 66)))) != 0) || ((((_la - 133)) & ~0x3f) == 0 && ((1L << (_la - 133)) & ((1L << (FIRST_VALUE - 133)) | (1L << (LAST_VALUE - 133)) | (1L << (LAG - 133)) | (1L << (LEAD - 133)) | (1L << (RATIO_TO_REPORT - 133)) | (1L << (FILL_TIME_SERIES - 133)) | (1L << (FLOW_TO_STOCK - 133)) | (1L << (STOCK_TO_FLOW - 133)) | (1L << (TIMESHIFT - 133)) | (1L << (INNER_JOIN - 133)) | (1L << (LEFT_JOIN - 133)) | (1L << (CROSS_JOIN - 133)) | (1L << (FULL_JOIN - 133)) | (1L << (PERIOD_INDICATOR - 133)))) != 0) || _la==IDENTIFIER) {
					{
					setState(1084);
					component();
					}
				}

				setState(1087);
				match(T__13);
				}
				break;
			case MEDIAN:
				enterOuterAlt(_localctx, 4);
				{
				setState(1088);
				((AggrFunctionContext)_localctx).opSymbol = match(MEDIAN);
				setState(1089);
				match(T__12);
				setState(1090);
				component();
				setState(1091);
				match(T__13);
				}
				break;
			case MIN:
				enterOuterAlt(_localctx, 5);
				{
				setState(1093);
				((AggrFunctionContext)_localctx).opSymbol = match(MIN);
				setState(1094);
				match(T__12);
				setState(1095);
				component();
				setState(1096);
				match(T__13);
				}
				break;
			case MAX:
				enterOuterAlt(_localctx, 6);
				{
				setState(1098);
				((AggrFunctionContext)_localctx).opSymbol = match(MAX);
				setState(1099);
				match(T__12);
				setState(1100);
				component();
				setState(1101);
				match(T__13);
				}
				break;
			case RANK:
				enterOuterAlt(_localctx, 7);
				{
				setState(1103);
				((AggrFunctionContext)_localctx).opSymbol = match(RANK);
				setState(1104);
				match(T__12);
				setState(1105);
				component();
				setState(1106);
				match(T__13);
				}
				break;
			case STDDEV_POP:
				enterOuterAlt(_localctx, 8);
				{
				setState(1108);
				((AggrFunctionContext)_localctx).opSymbol = match(STDDEV_POP);
				setState(1109);
				match(T__12);
				setState(1110);
				component();
				setState(1111);
				match(T__13);
				}
				break;
			case STDDEV_SAMP:
				enterOuterAlt(_localctx, 9);
				{
				setState(1113);
				((AggrFunctionContext)_localctx).opSymbol = match(STDDEV_SAMP);
				setState(1114);
				match(T__12);
				setState(1115);
				component();
				setState(1116);
				match(T__13);
				}
				break;
			case VAR_POP:
				enterOuterAlt(_localctx, 10);
				{
				setState(1118);
				((AggrFunctionContext)_localctx).opSymbol = match(VAR_POP);
				setState(1119);
				match(T__12);
				setState(1120);
				component();
				setState(1121);
				match(T__13);
				}
				break;
			case VAR_SAMP:
				enterOuterAlt(_localctx, 11);
				{
				setState(1123);
				((AggrFunctionContext)_localctx).opSymbol = match(VAR_SAMP);
				setState(1124);
				match(T__12);
				setState(1125);
				component();
				setState(1126);
				match(T__13);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AggrFunctionNameContext extends ParserRuleContext {
		public TerminalNode SUM() { return getToken(VtlParser.SUM, 0); }
		public TerminalNode AVG() { return getToken(VtlParser.AVG, 0); }
		public TerminalNode COUNT() { return getToken(VtlParser.COUNT, 0); }
		public TerminalNode MEDIAN() { return getToken(VtlParser.MEDIAN, 0); }
		public TerminalNode MIN() { return getToken(VtlParser.MIN, 0); }
		public TerminalNode MAX() { return getToken(VtlParser.MAX, 0); }
		public TerminalNode STDDEV_POP() { return getToken(VtlParser.STDDEV_POP, 0); }
		public TerminalNode STDDEV_SAMP() { return getToken(VtlParser.STDDEV_SAMP, 0); }
		public TerminalNode VAR_POP() { return getToken(VtlParser.VAR_POP, 0); }
		public TerminalNode VAR_SAMP() { return getToken(VtlParser.VAR_SAMP, 0); }
		public AggrFunctionNameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_aggrFunctionName; }
	}

	public final AggrFunctionNameContext aggrFunctionName() throws RecognitionException {
		AggrFunctionNameContext _localctx = new AggrFunctionNameContext(_ctx, getState());
		enterRule(_localctx, 86, RULE_aggrFunctionName);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1130);
			_la = _input.LA(1);
			if ( !(((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (MIN - 66)) | (1L << (MAX - 66)) | (1L << (SUM - 66)) | (1L << (AVG - 66)) | (1L << (MEDIAN - 66)) | (1L << (COUNT - 66)) | (1L << (STDDEV_POP - 66)) | (1L << (STDDEV_SAMP - 66)) | (1L << (VAR_POP - 66)) | (1L << (VAR_SAMP - 66)))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class GroupingClauseContext extends ParserRuleContext {
		public GroupKeywordContext groupKeyword() {
			return getRuleContext(GroupKeywordContext.class,0);
		}
		public List<ComponentContext> component() {
			return getRuleContexts(ComponentContext.class);
		}
		public ComponentContext component(int i) {
			return getRuleContext(ComponentContext.class,i);
		}
		public GroupingClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_groupingClause; }
	}

	public final GroupingClauseContext groupingClause() throws RecognitionException {
		GroupingClauseContext _localctx = new GroupingClauseContext(_ctx, getState());
		enterRule(_localctx, 88, RULE_groupingClause);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1132);
			groupKeyword();
			setState(1133);
			component();
			setState(1138);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==CARTESIAN_PER) {
				{
				{
				setState(1134);
				match(CARTESIAN_PER);
				setState(1135);
				component();
				}
				}
				setState(1140);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class HavingClauseContext extends ParserRuleContext {
		public TerminalNode HAVING() { return getToken(VtlParser.HAVING, 0); }
		public HavingExprContext havingExpr() {
			return getRuleContext(HavingExprContext.class,0);
		}
		public HavingClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_havingClause; }
	}

	public final HavingClauseContext havingClause() throws RecognitionException {
		HavingClauseContext _localctx = new HavingClauseContext(_ctx, getState());
		enterRule(_localctx, 90, RULE_havingClause);
		try {
			setState(1148);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,97,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(1141);
				match(HAVING);
				setState(1142);
				havingExpr(0);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(1143);
				match(HAVING);
				setState(1144);
				match(T__12);
				setState(1145);
				havingExpr(0);
				setState(1146);
				match(T__13);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class HavingExprContext extends ParserRuleContext {
		public ScalarContext leftScalar;
		public Token opSymbol;
		public AggrFunctionContext leftAggrFunction;
		public AggrFunctionContext aggrFunction() {
			return getRuleContext(AggrFunctionContext.class,0);
		}
		public ScalarContext scalar() {
			return getRuleContext(ScalarContext.class,0);
		}
		public List<HavingExprContext> havingExpr() {
			return getRuleContexts(HavingExprContext.class);
		}
		public HavingExprContext havingExpr(int i) {
			return getRuleContext(HavingExprContext.class,i);
		}
		public TerminalNode AND() { return getToken(VtlParser.AND, 0); }
		public TerminalNode OR() { return getToken(VtlParser.OR, 0); }
		public TerminalNode XOR() { return getToken(VtlParser.XOR, 0); }
		public HavingExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_havingExpr; }
	}

	public final HavingExprContext havingExpr() throws RecognitionException {
		return havingExpr(0);
	}

	private HavingExprContext havingExpr(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		HavingExprContext _localctx = new HavingExprContext(_ctx, _parentState);
		HavingExprContext _prevctx = _localctx;
		int _startState = 92;
		enterRecursionRule(_localctx, 92, RULE_havingExpr, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(1159);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,98,_ctx) ) {
			case 1:
				{
				setState(1151);
				((HavingExprContext)_localctx).leftScalar = scalar(0);
				setState(1152);
				((HavingExprContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__4) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__9))) != 0)) ) {
					((HavingExprContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(1153);
				aggrFunction();
				}
				break;
			case 2:
				{
				setState(1155);
				((HavingExprContext)_localctx).leftAggrFunction = aggrFunction();
				setState(1156);
				((HavingExprContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__4) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__9))) != 0)) ) {
					((HavingExprContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(1157);
				scalar(0);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(1166);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,99,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					{
					_localctx = new HavingExprContext(_parentctx, _parentState);
					pushNewRecursionContext(_localctx, _startState, RULE_havingExpr);
					setState(1161);
					if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
					setState(1162);
					((HavingExprContext)_localctx).opSymbol = _input.LT(1);
					_la = _input.LA(1);
					if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << AND) | (1L << OR) | (1L << XOR))) != 0)) ) {
						((HavingExprContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
					}
					else {
						if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
						_errHandler.reportMatch(this);
						consume();
					}
					setState(1163);
					havingExpr(2);
					}
					} 
				}
				setState(1168);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,99,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class AnalyticInvocationContext extends ParserRuleContext {
		public AggrFunctionNameContext aggrOpSymbol;
		public Token opSymbol;
		public DatasetContext dataset() {
			return getRuleContext(DatasetContext.class,0);
		}
		public TerminalNode OVER() { return getToken(VtlParser.OVER, 0); }
		public AnalyticClauseContext analyticClause() {
			return getRuleContext(AnalyticClauseContext.class,0);
		}
		public AggrFunctionNameContext aggrFunctionName() {
			return getRuleContext(AggrFunctionNameContext.class,0);
		}
		public TerminalNode FIRST_VALUE() { return getToken(VtlParser.FIRST_VALUE, 0); }
		public TerminalNode LAST_VALUE() { return getToken(VtlParser.LAST_VALUE, 0); }
		public PartitionClauseContext partitionClause() {
			return getRuleContext(PartitionClauseContext.class,0);
		}
		public TerminalNode RATIO_TO_REPORT() { return getToken(VtlParser.RATIO_TO_REPORT, 0); }
		public List<ScalarContext> scalar() {
			return getRuleContexts(ScalarContext.class);
		}
		public ScalarContext scalar(int i) {
			return getRuleContext(ScalarContext.class,i);
		}
		public OrderClauseContext orderClause() {
			return getRuleContext(OrderClauseContext.class,0);
		}
		public TerminalNode LAG() { return getToken(VtlParser.LAG, 0); }
		public TerminalNode LEAD() { return getToken(VtlParser.LEAD, 0); }
		public AnalyticInvocationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_analyticInvocation; }
	}

	public final AnalyticInvocationContext analyticInvocation() throws RecognitionException {
		AnalyticInvocationContext _localctx = new AnalyticInvocationContext(_ctx, getState());
		enterRule(_localctx, 94, RULE_analyticInvocation);
		int _la;
		try {
			setState(1214);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case MIN:
			case MAX:
			case SUM:
			case AVG:
			case MEDIAN:
			case COUNT:
			case STDDEV_POP:
			case STDDEV_SAMP:
			case VAR_POP:
			case VAR_SAMP:
				enterOuterAlt(_localctx, 1);
				{
				setState(1169);
				((AnalyticInvocationContext)_localctx).aggrOpSymbol = aggrFunctionName();
				setState(1170);
				match(T__12);
				setState(1171);
				dataset();
				setState(1172);
				match(OVER);
				setState(1173);
				match(T__12);
				setState(1174);
				analyticClause();
				setState(1175);
				match(T__13);
				setState(1176);
				match(T__13);
				}
				break;
			case FIRST_VALUE:
			case LAST_VALUE:
				enterOuterAlt(_localctx, 2);
				{
				setState(1178);
				((AnalyticInvocationContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !(_la==FIRST_VALUE || _la==LAST_VALUE) ) {
					((AnalyticInvocationContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(1179);
				match(T__12);
				setState(1180);
				dataset();
				setState(1181);
				match(OVER);
				setState(1182);
				match(T__12);
				setState(1183);
				analyticClause();
				setState(1184);
				match(T__13);
				setState(1185);
				match(T__13);
				}
				break;
			case RATIO_TO_REPORT:
				enterOuterAlt(_localctx, 3);
				{
				setState(1187);
				((AnalyticInvocationContext)_localctx).opSymbol = match(RATIO_TO_REPORT);
				setState(1188);
				match(T__12);
				setState(1189);
				dataset();
				setState(1190);
				match(OVER);
				setState(1191);
				match(T__12);
				setState(1192);
				partitionClause();
				setState(1193);
				match(T__13);
				setState(1194);
				match(T__13);
				}
				break;
			case LAG:
			case LEAD:
				enterOuterAlt(_localctx, 4);
				{
				setState(1196);
				((AnalyticInvocationContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !(_la==LAG || _la==LEAD) ) {
					((AnalyticInvocationContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(1197);
				match(T__12);
				setState(1198);
				dataset();
				setState(1199);
				match(CARTESIAN_PER);
				setState(1200);
				scalar(0);
				setState(1203);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==CARTESIAN_PER) {
					{
					setState(1201);
					match(CARTESIAN_PER);
					setState(1202);
					scalar(0);
					}
				}

				setState(1205);
				match(OVER);
				setState(1206);
				match(T__12);
				setState(1208);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==PARTITION) {
					{
					setState(1207);
					partitionClause();
					}
				}

				setState(1210);
				orderClause();
				setState(1211);
				match(T__13);
				setState(1212);
				match(T__13);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AnalyticFunctionContext extends ParserRuleContext {
		public Token opSymbol;
		public AggrFunctionNameContext aggrFunctionName() {
			return getRuleContext(AggrFunctionNameContext.class,0);
		}
		public ComponentContext component() {
			return getRuleContext(ComponentContext.class,0);
		}
		public TerminalNode OVER() { return getToken(VtlParser.OVER, 0); }
		public AnalyticClauseContext analyticClause() {
			return getRuleContext(AnalyticClauseContext.class,0);
		}
		public TerminalNode FIRST_VALUE() { return getToken(VtlParser.FIRST_VALUE, 0); }
		public TerminalNode LAST_VALUE() { return getToken(VtlParser.LAST_VALUE, 0); }
		public OrderClauseContext orderClause() {
			return getRuleContext(OrderClauseContext.class,0);
		}
		public TerminalNode RANK() { return getToken(VtlParser.RANK, 0); }
		public PartitionClauseContext partitionClause() {
			return getRuleContext(PartitionClauseContext.class,0);
		}
		public TerminalNode RATIO_TO_REPORT() { return getToken(VtlParser.RATIO_TO_REPORT, 0); }
		public List<ScalarContext> scalar() {
			return getRuleContexts(ScalarContext.class);
		}
		public ScalarContext scalar(int i) {
			return getRuleContext(ScalarContext.class,i);
		}
		public TerminalNode LAG() { return getToken(VtlParser.LAG, 0); }
		public TerminalNode LEAD() { return getToken(VtlParser.LEAD, 0); }
		public AnalyticFunctionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_analyticFunction; }
	}

	public final AnalyticFunctionContext analyticFunction() throws RecognitionException {
		AnalyticFunctionContext _localctx = new AnalyticFunctionContext(_ctx, getState());
		enterRule(_localctx, 96, RULE_analyticFunction);
		int _la;
		try {
			setState(1272);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case MIN:
			case MAX:
			case SUM:
			case AVG:
			case MEDIAN:
			case COUNT:
			case STDDEV_POP:
			case STDDEV_SAMP:
			case VAR_POP:
			case VAR_SAMP:
				enterOuterAlt(_localctx, 1);
				{
				setState(1216);
				aggrFunctionName();
				setState(1217);
				match(T__12);
				setState(1218);
				component();
				setState(1219);
				match(OVER);
				setState(1220);
				match(T__12);
				setState(1221);
				analyticClause();
				setState(1222);
				match(T__13);
				setState(1223);
				match(T__13);
				}
				break;
			case FIRST_VALUE:
			case LAST_VALUE:
				enterOuterAlt(_localctx, 2);
				{
				setState(1225);
				((AnalyticFunctionContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !(_la==FIRST_VALUE || _la==LAST_VALUE) ) {
					((AnalyticFunctionContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(1226);
				match(T__12);
				setState(1227);
				component();
				setState(1228);
				match(OVER);
				setState(1229);
				match(T__12);
				setState(1230);
				analyticClause();
				setState(1231);
				match(T__13);
				setState(1232);
				match(T__13);
				}
				break;
			case RANK:
				enterOuterAlt(_localctx, 3);
				{
				setState(1234);
				((AnalyticFunctionContext)_localctx).opSymbol = match(RANK);
				setState(1235);
				match(T__12);
				setState(1236);
				match(OVER);
				setState(1237);
				match(T__12);
				setState(1239);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==PARTITION) {
					{
					setState(1238);
					partitionClause();
					}
				}

				setState(1241);
				orderClause();
				setState(1242);
				match(T__13);
				setState(1243);
				match(T__13);
				}
				break;
			case RATIO_TO_REPORT:
				enterOuterAlt(_localctx, 4);
				{
				setState(1245);
				((AnalyticFunctionContext)_localctx).opSymbol = match(RATIO_TO_REPORT);
				setState(1246);
				match(T__12);
				setState(1247);
				component();
				setState(1248);
				match(OVER);
				setState(1249);
				match(T__12);
				setState(1250);
				partitionClause();
				setState(1251);
				match(T__13);
				setState(1252);
				match(T__13);
				}
				break;
			case LAG:
			case LEAD:
				enterOuterAlt(_localctx, 5);
				{
				setState(1254);
				((AnalyticFunctionContext)_localctx).opSymbol = _input.LT(1);
				_la = _input.LA(1);
				if ( !(_la==LAG || _la==LEAD) ) {
					((AnalyticFunctionContext)_localctx).opSymbol = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(1255);
				match(T__12);
				setState(1256);
				component();
				setState(1257);
				match(CARTESIAN_PER);
				setState(1258);
				scalar(0);
				setState(1261);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==CARTESIAN_PER) {
					{
					setState(1259);
					match(CARTESIAN_PER);
					setState(1260);
					scalar(0);
					}
				}

				setState(1263);
				match(OVER);
				setState(1264);
				match(T__12);
				setState(1266);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==PARTITION) {
					{
					setState(1265);
					partitionClause();
					}
				}

				setState(1268);
				orderClause();
				setState(1269);
				match(T__13);
				setState(1270);
				match(T__13);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AnalyticClauseContext extends ParserRuleContext {
		public PartitionClauseContext partitionClause() {
			return getRuleContext(PartitionClauseContext.class,0);
		}
		public OrderClauseContext orderClause() {
			return getRuleContext(OrderClauseContext.class,0);
		}
		public WindowingClauseContext windowingClause() {
			return getRuleContext(WindowingClauseContext.class,0);
		}
		public AnalyticClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_analyticClause; }
	}

	public final AnalyticClauseContext analyticClause() throws RecognitionException {
		AnalyticClauseContext _localctx = new AnalyticClauseContext(_ctx, getState());
		enterRule(_localctx, 98, RULE_analyticClause);
		int _la;
		try {
			setState(1295);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,113,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(1274);
				partitionClause();
				setState(1276);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==ORDER) {
					{
					setState(1275);
					orderClause();
					}
				}

				setState(1279);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==DATA || _la==RANGE) {
					{
					setState(1278);
					windowingClause();
					}
				}

				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(1282);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==PARTITION) {
					{
					setState(1281);
					partitionClause();
					}
				}

				setState(1284);
				orderClause();
				setState(1286);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==DATA || _la==RANGE) {
					{
					setState(1285);
					windowingClause();
					}
				}

				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(1289);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==PARTITION) {
					{
					setState(1288);
					partitionClause();
					}
				}

				setState(1292);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==ORDER) {
					{
					setState(1291);
					orderClause();
					}
				}

				setState(1294);
				windowingClause();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PartitionClauseContext extends ParserRuleContext {
		public TerminalNode PARTITION() { return getToken(VtlParser.PARTITION, 0); }
		public TerminalNode BY() { return getToken(VtlParser.BY, 0); }
		public List<ComponentContext> component() {
			return getRuleContexts(ComponentContext.class);
		}
		public ComponentContext component(int i) {
			return getRuleContext(ComponentContext.class,i);
		}
		public PartitionClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_partitionClause; }
	}

	public final PartitionClauseContext partitionClause() throws RecognitionException {
		PartitionClauseContext _localctx = new PartitionClauseContext(_ctx, getState());
		enterRule(_localctx, 100, RULE_partitionClause);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1297);
			match(PARTITION);
			setState(1298);
			match(BY);
			setState(1299);
			component();
			setState(1304);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==CARTESIAN_PER) {
				{
				{
				setState(1300);
				match(CARTESIAN_PER);
				setState(1301);
				component();
				}
				}
				setState(1306);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class OrderClauseContext extends ParserRuleContext {
		public TerminalNode ORDER() { return getToken(VtlParser.ORDER, 0); }
		public TerminalNode BY() { return getToken(VtlParser.BY, 0); }
		public List<OrderExprContext> orderExpr() {
			return getRuleContexts(OrderExprContext.class);
		}
		public OrderExprContext orderExpr(int i) {
			return getRuleContext(OrderExprContext.class,i);
		}
		public OrderClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_orderClause; }
	}

	public final OrderClauseContext orderClause() throws RecognitionException {
		OrderClauseContext _localctx = new OrderClauseContext(_ctx, getState());
		enterRule(_localctx, 102, RULE_orderClause);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1307);
			match(ORDER);
			setState(1308);
			match(BY);
			setState(1309);
			orderExpr();
			setState(1314);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==CARTESIAN_PER) {
				{
				{
				setState(1310);
				match(CARTESIAN_PER);
				setState(1311);
				orderExpr();
				}
				}
				setState(1316);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class OrderExprContext extends ParserRuleContext {
		public ComponentContext component() {
			return getRuleContext(ComponentContext.class,0);
		}
		public TerminalNode ASC() { return getToken(VtlParser.ASC, 0); }
		public TerminalNode DESC() { return getToken(VtlParser.DESC, 0); }
		public OrderExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_orderExpr; }
	}

	public final OrderExprContext orderExpr() throws RecognitionException {
		OrderExprContext _localctx = new OrderExprContext(_ctx, getState());
		enterRule(_localctx, 104, RULE_orderExpr);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1317);
			component();
			setState(1319);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==ASC || _la==DESC) {
				{
				setState(1318);
				_la = _input.LA(1);
				if ( !(_la==ASC || _la==DESC) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class WindowingClauseContext extends ParserRuleContext {
		public TerminalNode BETWEEN() { return getToken(VtlParser.BETWEEN, 0); }
		public FirstWindowLimitContext firstWindowLimit() {
			return getRuleContext(FirstWindowLimitContext.class,0);
		}
		public TerminalNode AND() { return getToken(VtlParser.AND, 0); }
		public SecondWindowLimitContext secondWindowLimit() {
			return getRuleContext(SecondWindowLimitContext.class,0);
		}
		public TerminalNode RANGE() { return getToken(VtlParser.RANGE, 0); }
		public TerminalNode DATA() { return getToken(VtlParser.DATA, 0); }
		public TerminalNode POINTS() { return getToken(VtlParser.POINTS, 0); }
		public WindowingClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_windowingClause; }
	}

	public final WindowingClauseContext windowingClause() throws RecognitionException {
		WindowingClauseContext _localctx = new WindowingClauseContext(_ctx, getState());
		enterRule(_localctx, 106, RULE_windowingClause);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1324);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case DATA:
				{
				{
				setState(1321);
				match(DATA);
				setState(1322);
				match(POINTS);
				}
				}
				break;
			case RANGE:
				{
				setState(1323);
				match(RANGE);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(1326);
			match(BETWEEN);
			setState(1327);
			firstWindowLimit();
			setState(1328);
			match(AND);
			setState(1329);
			secondWindowLimit();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FirstWindowLimitContext extends ParserRuleContext {
		public TerminalNode INTEGER_CONSTANT() { return getToken(VtlParser.INTEGER_CONSTANT, 0); }
		public TerminalNode PRECEDING() { return getToken(VtlParser.PRECEDING, 0); }
		public TerminalNode CURRENT() { return getToken(VtlParser.CURRENT, 0); }
		public TerminalNode DATA() { return getToken(VtlParser.DATA, 0); }
		public TerminalNode POINT() { return getToken(VtlParser.POINT, 0); }
		public TerminalNode UNBOUNDED() { return getToken(VtlParser.UNBOUNDED, 0); }
		public FirstWindowLimitContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_firstWindowLimit; }
	}

	public final FirstWindowLimitContext firstWindowLimit() throws RecognitionException {
		FirstWindowLimitContext _localctx = new FirstWindowLimitContext(_ctx, getState());
		enterRule(_localctx, 108, RULE_firstWindowLimit);
		try {
			setState(1338);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case INTEGER_CONSTANT:
				enterOuterAlt(_localctx, 1);
				{
				setState(1331);
				match(INTEGER_CONSTANT);
				setState(1332);
				match(PRECEDING);
				}
				break;
			case CURRENT:
				enterOuterAlt(_localctx, 2);
				{
				setState(1333);
				match(CURRENT);
				setState(1334);
				match(DATA);
				setState(1335);
				match(POINT);
				}
				break;
			case UNBOUNDED:
				enterOuterAlt(_localctx, 3);
				{
				setState(1336);
				match(UNBOUNDED);
				setState(1337);
				match(PRECEDING);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class SecondWindowLimitContext extends ParserRuleContext {
		public TerminalNode INTEGER_CONSTANT() { return getToken(VtlParser.INTEGER_CONSTANT, 0); }
		public TerminalNode FOLLOWING() { return getToken(VtlParser.FOLLOWING, 0); }
		public TerminalNode CURRENT() { return getToken(VtlParser.CURRENT, 0); }
		public TerminalNode DATA() { return getToken(VtlParser.DATA, 0); }
		public TerminalNode POINT() { return getToken(VtlParser.POINT, 0); }
		public TerminalNode UNBOUNDED() { return getToken(VtlParser.UNBOUNDED, 0); }
		public SecondWindowLimitContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_secondWindowLimit; }
	}

	public final SecondWindowLimitContext secondWindowLimit() throws RecognitionException {
		SecondWindowLimitContext _localctx = new SecondWindowLimitContext(_ctx, getState());
		enterRule(_localctx, 110, RULE_secondWindowLimit);
		try {
			setState(1347);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case INTEGER_CONSTANT:
				enterOuterAlt(_localctx, 1);
				{
				setState(1340);
				match(INTEGER_CONSTANT);
				setState(1341);
				match(FOLLOWING);
				}
				break;
			case CURRENT:
				enterOuterAlt(_localctx, 2);
				{
				setState(1342);
				match(CURRENT);
				setState(1343);
				match(DATA);
				setState(1344);
				match(POINT);
				}
				break;
			case UNBOUNDED:
				enterOuterAlt(_localctx, 3);
				{
				setState(1345);
				match(UNBOUNDED);
				setState(1346);
				match(FOLLOWING);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AnalyticFunctionNameContext extends ParserRuleContext {
		public TerminalNode FIRST_VALUE() { return getToken(VtlParser.FIRST_VALUE, 0); }
		public TerminalNode LAST_VALUE() { return getToken(VtlParser.LAST_VALUE, 0); }
		public TerminalNode LAG() { return getToken(VtlParser.LAG, 0); }
		public TerminalNode RANK() { return getToken(VtlParser.RANK, 0); }
		public TerminalNode RATIO_TO_REPORT() { return getToken(VtlParser.RATIO_TO_REPORT, 0); }
		public TerminalNode LEAD() { return getToken(VtlParser.LEAD, 0); }
		public AnalyticFunctionNameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_analyticFunctionName; }
	}

	public final AnalyticFunctionNameContext analyticFunctionName() throws RecognitionException {
		AnalyticFunctionNameContext _localctx = new AnalyticFunctionNameContext(_ctx, getState());
		enterRule(_localctx, 112, RULE_analyticFunctionName);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1349);
			_la = _input.LA(1);
			if ( !(_la==RANK || ((((_la - 133)) & ~0x3f) == 0 && ((1L << (_la - 133)) & ((1L << (FIRST_VALUE - 133)) | (1L << (LAST_VALUE - 133)) | (1L << (LAG - 133)) | (1L << (LEAD - 133)) | (1L << (RATIO_TO_REPORT - 133)))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ListContext extends ParserRuleContext {
		public List<ScalarContext> scalar() {
			return getRuleContexts(ScalarContext.class);
		}
		public ScalarContext scalar(int i) {
			return getRuleContext(ScalarContext.class,i);
		}
		public ListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_list; }
	}

	public final ListContext list() throws RecognitionException {
		ListContext _localctx = new ListContext(_ctx, getState());
		enterRule(_localctx, 114, RULE_list);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1351);
			match(T__14);
			setState(1352);
			scalar(0);
			setState(1357);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==CARTESIAN_PER) {
				{
				{
				setState(1353);
				match(CARTESIAN_PER);
				setState(1354);
				scalar(0);
				}
				}
				setState(1359);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(1360);
			match(T__15);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class VarIDContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(VtlParser.IDENTIFIER, 0); }
		public VarIDContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_varID; }
	}

	public final VarIDContext varID() throws RecognitionException {
		VarIDContext _localctx = new VarIDContext(_ctx, getState());
		enterRule(_localctx, 116, RULE_varID);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1362);
			match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DatasetIDContext extends ParserRuleContext {
		public List<TerminalNode> IDENTIFIER() { return getTokens(VtlParser.IDENTIFIER); }
		public TerminalNode IDENTIFIER(int i) {
			return getToken(VtlParser.IDENTIFIER, i);
		}
		public DatasetIDContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_datasetID; }
	}

	public final DatasetIDContext datasetID() throws RecognitionException {
		DatasetIDContext _localctx = new DatasetIDContext(_ctx, getState());
		enterRule(_localctx, 118, RULE_datasetID);
		try {
			setState(1368);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,121,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(1364);
				match(IDENTIFIER);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(1365);
				match(IDENTIFIER);
				setState(1366);
				match(T__16);
				setState(1367);
				match(IDENTIFIER);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ComponentIDContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(VtlParser.IDENTIFIER, 0); }
		public ComponentIDContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_componentID; }
	}

	public final ComponentIDContext componentID() throws RecognitionException {
		ComponentIDContext _localctx = new ComponentIDContext(_ctx, getState());
		enterRule(_localctx, 120, RULE_componentID);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1370);
			match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JoinKeywordContext extends ParserRuleContext {
		public TerminalNode INNER_JOIN() { return getToken(VtlParser.INNER_JOIN, 0); }
		public TerminalNode LEFT_JOIN() { return getToken(VtlParser.LEFT_JOIN, 0); }
		public TerminalNode FULL_JOIN() { return getToken(VtlParser.FULL_JOIN, 0); }
		public TerminalNode CROSS_JOIN() { return getToken(VtlParser.CROSS_JOIN, 0); }
		public JoinKeywordContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_joinKeyword; }
	}

	public final JoinKeywordContext joinKeyword() throws RecognitionException {
		JoinKeywordContext _localctx = new JoinKeywordContext(_ctx, getState());
		enterRule(_localctx, 122, RULE_joinKeyword);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1372);
			_la = _input.LA(1);
			if ( !(((((_la - 178)) & ~0x3f) == 0 && ((1L << (_la - 178)) & ((1L << (INNER_JOIN - 178)) | (1L << (LEFT_JOIN - 178)) | (1L << (CROSS_JOIN - 178)) | (1L << (FULL_JOIN - 178)))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class GroupKeywordContext extends ParserRuleContext {
		public TerminalNode GROUP() { return getToken(VtlParser.GROUP, 0); }
		public TerminalNode BY() { return getToken(VtlParser.BY, 0); }
		public TerminalNode EXCEPT() { return getToken(VtlParser.EXCEPT, 0); }
		public TerminalNode ALL() { return getToken(VtlParser.ALL, 0); }
		public GroupKeywordContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_groupKeyword; }
	}

	public final GroupKeywordContext groupKeyword() throws RecognitionException {
		GroupKeywordContext _localctx = new GroupKeywordContext(_ctx, getState());
		enterRule(_localctx, 124, RULE_groupKeyword);
		try {
			setState(1380);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,122,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(1374);
				match(GROUP);
				setState(1375);
				match(BY);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(1376);
				match(GROUP);
				setState(1377);
				match(EXCEPT);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(1378);
				match(GROUP);
				setState(1379);
				match(ALL);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ConstantContext extends ParserRuleContext {
		public TerminalNode FLOAT_CONSTANT() { return getToken(VtlParser.FLOAT_CONSTANT, 0); }
		public TerminalNode INTEGER_CONSTANT() { return getToken(VtlParser.INTEGER_CONSTANT, 0); }
		public TerminalNode BOOLEAN_CONSTANT() { return getToken(VtlParser.BOOLEAN_CONSTANT, 0); }
		public TerminalNode STRING_CONSTANT() { return getToken(VtlParser.STRING_CONSTANT, 0); }
		public TerminalNode TIME_CONSTANT() { return getToken(VtlParser.TIME_CONSTANT, 0); }
		public TerminalNode NULL_CONSTANT() { return getToken(VtlParser.NULL_CONSTANT, 0); }
		public ConstantContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_constant; }
	}

	public final ConstantContext constant() throws RecognitionException {
		ConstantContext _localctx = new ConstantContext(_ctx, getState());
		enterRule(_localctx, 126, RULE_constant);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1382);
			_la = _input.LA(1);
			if ( !(((((_la - 225)) & ~0x3f) == 0 && ((1L << (_la - 225)) & ((1L << (INTEGER_CONSTANT - 225)) | (1L << (FLOAT_CONSTANT - 225)) | (1L << (BOOLEAN_CONSTANT - 225)) | (1L << (NULL_CONSTANT - 225)) | (1L << (STRING_CONSTANT - 225)) | (1L << (TIME_CONSTANT - 225)))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ComponentRoleContext extends ParserRuleContext {
		public TerminalNode MEASURE() { return getToken(VtlParser.MEASURE, 0); }
		public TerminalNode DIMENSION() { return getToken(VtlParser.DIMENSION, 0); }
		public TerminalNode ATTRIBUTE() { return getToken(VtlParser.ATTRIBUTE, 0); }
		public TerminalNode VIRAL() { return getToken(VtlParser.VIRAL, 0); }
		public ComponentRoleContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_componentRole; }
	}

	public final ComponentRoleContext componentRole() throws RecognitionException {
		ComponentRoleContext _localctx = new ComponentRoleContext(_ctx, getState());
		enterRule(_localctx, 128, RULE_componentRole);
		try {
			setState(1389);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case MEASURE:
				enterOuterAlt(_localctx, 1);
				{
				setState(1384);
				match(MEASURE);
				}
				break;
			case DIMENSION:
				enterOuterAlt(_localctx, 2);
				{
				setState(1385);
				match(DIMENSION);
				}
				break;
			case ATTRIBUTE:
				enterOuterAlt(_localctx, 3);
				{
				setState(1386);
				match(ATTRIBUTE);
				}
				break;
			case VIRAL:
				enterOuterAlt(_localctx, 4);
				{
				setState(1387);
				match(VIRAL);
				setState(1388);
				match(ATTRIBUTE);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ValueDomainNameContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(VtlParser.IDENTIFIER, 0); }
		public ValueDomainNameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_valueDomainName; }
	}

	public final ValueDomainNameContext valueDomainName() throws RecognitionException {
		ValueDomainNameContext _localctx = new ValueDomainNameContext(_ctx, getState());
		enterRule(_localctx, 130, RULE_valueDomainName);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1391);
			match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class RetainTypeContext extends ParserRuleContext {
		public TerminalNode BOOLEAN_CONSTANT() { return getToken(VtlParser.BOOLEAN_CONSTANT, 0); }
		public TerminalNode ALL() { return getToken(VtlParser.ALL, 0); }
		public RetainTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_retainType; }
	}

	public final RetainTypeContext retainType() throws RecognitionException {
		RetainTypeContext _localctx = new RetainTypeContext(_ctx, getState());
		enterRule(_localctx, 132, RULE_retainType);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1393);
			_la = _input.LA(1);
			if ( !(_la==ALL || _la==BOOLEAN_CONSTANT) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LimitsMethodContext extends ParserRuleContext {
		public TerminalNode ALL() { return getToken(VtlParser.ALL, 0); }
		public TerminalNode SINGLE() { return getToken(VtlParser.SINGLE, 0); }
		public LimitsMethodContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_limitsMethod; }
	}

	public final LimitsMethodContext limitsMethod() throws RecognitionException {
		LimitsMethodContext _localctx = new LimitsMethodContext(_ctx, getState());
		enterRule(_localctx, 134, RULE_limitsMethod);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(1395);
			_la = _input.LA(1);
			if ( !(_la==ALL || _la==SINGLE) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 3:
			return openedDataset_sempred((OpenedDatasetContext)_localctx, predIndex);
		case 4:
			return closedDataset_sempred((ClosedDatasetContext)_localctx, predIndex);
		case 10:
			return scalar_sempred((ScalarContext)_localctx, predIndex);
		case 46:
			return havingExpr_sempred((HavingExprContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean openedDataset_sempred(OpenedDatasetContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 9);
		case 1:
			return precpred(_ctx, 8);
		case 2:
			return precpred(_ctx, 7);
		case 3:
			return precpred(_ctx, 6);
		case 4:
			return precpred(_ctx, 5);
		}
		return true;
	}
	private boolean closedDataset_sempred(ClosedDatasetContext _localctx, int predIndex) {
		switch (predIndex) {
		case 5:
			return precpred(_ctx, 36);
		}
		return true;
	}
	private boolean scalar_sempred(ScalarContext _localctx, int predIndex) {
		switch (predIndex) {
		case 6:
			return precpred(_ctx, 31);
		case 7:
			return precpred(_ctx, 30);
		case 8:
			return precpred(_ctx, 29);
		case 9:
			return precpred(_ctx, 28);
		case 10:
			return precpred(_ctx, 27);
		}
		return true;
	}
	private boolean havingExpr_sempred(HavingExprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 11:
			return precpred(_ctx, 1);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\u00fc\u0578\4\2\t"+
		"\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\4\65\t\65\4\66\t\66\4\67\t\67\48\t8\49\t9\4:\t:\4;\t;\4<\t<\4=\t="+
		"\4>\t>\4?\t?\4@\t@\4A\tA\4B\tB\4C\tC\4D\tD\4E\tE\3\2\5\2\u008c\n\2\3\2"+
		"\7\2\u008f\n\2\f\2\16\2\u0092\13\2\3\2\7\2\u0095\n\2\f\2\16\2\u0098\13"+
		"\2\3\2\7\2\u009b\n\2\f\2\16\2\u009e\13\2\3\2\5\2\u00a1\n\2\3\2\3\2\3\2"+
		"\5\2\u00a6\n\2\3\3\3\3\3\3\5\3\u00ab\n\3\3\3\3\3\5\3\u00af\n\3\3\3\3\3"+
		"\3\3\3\3\5\3\u00b5\n\3\3\4\3\4\3\4\5\4\u00ba\n\4\3\5\3\5\3\5\3\5\3\5\3"+
		"\5\3\5\3\5\3\5\3\5\5\5\u00c6\n\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\5\5\u00cf"+
		"\n\5\3\5\3\5\3\5\3\5\3\5\3\5\5\5\u00d7\n\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5"+
		"\5\5\u00e0\n\5\3\5\3\5\3\5\3\5\5\5\u00e6\n\5\3\5\3\5\5\5\u00ea\n\5\3\5"+
		"\3\5\3\5\3\5\5\5\u00f0\n\5\3\5\3\5\5\5\u00f4\n\5\3\5\3\5\3\5\3\5\5\5\u00fa"+
		"\n\5\3\5\3\5\5\5\u00fe\n\5\3\5\3\5\3\5\3\5\5\5\u0104\n\5\3\5\3\5\5\5\u0108"+
		"\n\5\3\5\3\5\3\5\3\5\5\5\u010e\n\5\5\5\u0110\n\5\3\5\3\5\3\5\3\5\3\5\3"+
		"\5\5\5\u0118\n\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\5\5\u0121\n\5\3\5\3\5\3\5"+
		"\3\5\3\5\3\5\5\5\u0129\n\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\5\5\u0132\n\5\3"+
		"\5\3\5\3\5\3\5\5\5\u0138\n\5\7\5\u013a\n\5\f\5\16\5\u013d\13\5\3\6\3\6"+
		"\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3"+
		"\6\3\6\3\6\3\6\3\6\3\6\3\6\5\6\u0159\n\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3"+
		"\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6"+
		"\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\5\6\u0182\n\6"+
		"\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3"+
		"\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6"+
		"\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3"+
		"\6\3\6\3\6\3\6\3\6\3\6\5\6\u01be\n\6\3\6\3\6\5\6\u01c2\n\6\3\6\3\6\3\6"+
		"\3\6\3\6\3\6\3\6\3\6\3\6\5\6\u01cd\n\6\3\6\3\6\5\6\u01d1\n\6\3\6\3\6\3"+
		"\6\3\6\3\6\3\6\3\6\3\6\3\6\5\6\u01dc\n\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3"+
		"\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\5\6\u01f2\n\6\3\6\3"+
		"\6\3\6\3\6\3\6\3\6\3\6\3\6\5\6\u01fc\n\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3"+
		"\6\3\6\5\6\u0207\n\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3"+
		"\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\5\6"+
		"\u0226\n\6\3\6\3\6\5\6\u022a\n\6\3\6\3\6\3\6\3\6\3\6\7\6\u0231\n\6\f\6"+
		"\16\6\u0234\13\6\3\7\3\7\3\7\3\7\3\b\3\b\3\b\3\b\5\b\u023e\n\b\3\t\3\t"+
		"\3\t\3\t\3\t\5\t\u0245\n\t\3\t\3\t\3\t\5\t\u024a\n\t\3\t\3\t\3\t\5\t\u024f"+
		"\n\t\3\t\3\t\3\t\3\t\3\t\5\t\u0256\n\t\3\t\3\t\3\t\5\t\u025b\n\t\3\t\3"+
		"\t\3\t\5\t\u0260\n\t\3\t\3\t\3\t\5\t\u0265\n\t\3\n\3\n\5\n\u0269\n\n\3"+
		"\13\3\13\3\13\5\13\u026e\n\13\3\13\3\13\3\f\3\f\3\f\5\f\u0275\n\f\3\f"+
		"\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\5\f\u0285\n\f\3\f"+
		"\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3"+
		"\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f"+
		"\3\f\3\f\3\f\5\f\u02ae\n\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f"+
		"\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3"+
		"\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f"+
		"\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\5\f\u02ea\n\f\3\f\3\f"+
		"\5\f\u02ee\n\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\5\f\u02f9\n\f\3\f\3"+
		"\f\5\f\u02fd\n\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\5\f\u0308\n\f\3\f"+
		"\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3"+
		"\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\5\f\u0329\n\f\3\f\3"+
		"\f\5\f\u032d\n\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3"+
		"\f\3\f\3\f\5\f\u033f\n\f\7\f\u0341\n\f\f\f\16\f\u0344\13\f\3\r\3\r\3\r"+
		"\3\r\3\r\3\r\3\r\3\16\3\16\5\16\u034f\n\16\3\17\3\17\3\17\3\17\3\17\7"+
		"\17\u0356\n\17\f\17\16\17\u0359\13\17\3\17\3\17\3\17\3\17\3\17\3\17\3"+
		"\17\3\17\3\17\3\17\3\17\3\17\3\17\3\17\3\17\3\17\3\17\3\17\3\17\3\17\3"+
		"\17\7\17\u0370\n\17\f\17\16\17\u0373\13\17\3\17\3\17\5\17\u0377\n\17\3"+
		"\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\5\20\u0383\n\20\3\21"+
		"\3\21\3\21\3\21\7\21\u0389\n\21\f\21\16\21\u038c\13\21\3\21\5\21\u038f"+
		"\n\21\3\21\5\21\u0392\n\21\3\22\5\22\u0395\n\22\3\22\3\22\3\22\3\22\3"+
		"\23\3\23\3\23\3\24\3\24\3\24\3\24\7\24\u03a2\n\24\f\24\16\24\u03a5\13"+
		"\24\3\25\3\25\3\25\3\25\3\26\3\26\3\26\3\26\7\26\u03af\n\26\f\26\16\26"+
		"\u03b2\13\26\3\27\5\27\u03b5\n\27\3\27\3\27\3\27\3\27\5\27\u03bb\n\27"+
		"\3\30\3\30\3\30\3\30\7\30\u03c1\n\30\f\30\16\30\u03c4\13\30\3\31\3\31"+
		"\3\31\3\31\7\31\u03ca\n\31\f\31\16\31\u03cd\13\31\3\32\3\32\3\32\3\32"+
		"\3\32\3\33\3\33\3\33\3\33\3\33\3\34\3\34\3\34\3\34\7\34\u03dd\n\34\f\34"+
		"\16\34\u03e0\13\34\3\35\3\35\3\35\3\35\3\36\3\36\3\36\3\36\5\36\u03ea"+
		"\n\36\3\36\3\36\3\37\3\37\5\37\u03f0\n\37\3 \5 \u03f3\n \3 \3 \3 \3 \3"+
		" \5 \u03fa\n \5 \u03fc\n \3 \3 \5 \u0400\n \3 \5 \u0403\n \3!\3!\3!\7"+
		"!\u0408\n!\f!\16!\u040b\13!\3\"\3\"\3\"\5\"\u0410\n\"\3#\3#\3#\3#\7#\u0416"+
		"\n#\f#\16#\u0419\13#\3$\3$\3%\3%\3&\3&\3\'\3\'\3(\3(\3)\3)\3*\3*\3*\3"+
		"+\3+\3+\3+\3+\5+\u042f\n+\3+\3+\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3"+
		",\5,\u0440\n,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3"+
		",\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\5"+
		",\u046b\n,\3-\3-\3.\3.\3.\3.\7.\u0473\n.\f.\16.\u0476\13.\3/\3/\3/\3/"+
		"\3/\3/\3/\5/\u047f\n/\3\60\3\60\3\60\3\60\3\60\3\60\3\60\3\60\3\60\5\60"+
		"\u048a\n\60\3\60\3\60\3\60\7\60\u048f\n\60\f\60\16\60\u0492\13\60\3\61"+
		"\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61"+
		"\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61"+
		"\3\61\3\61\3\61\3\61\3\61\5\61\u04b6\n\61\3\61\3\61\3\61\5\61\u04bb\n"+
		"\61\3\61\3\61\3\61\3\61\5\61\u04c1\n\61\3\62\3\62\3\62\3\62\3\62\3\62"+
		"\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62"+
		"\3\62\3\62\3\62\5\62\u04da\n\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62"+
		"\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\5\62\u04f0"+
		"\n\62\3\62\3\62\3\62\5\62\u04f5\n\62\3\62\3\62\3\62\3\62\5\62\u04fb\n"+
		"\62\3\63\3\63\5\63\u04ff\n\63\3\63\5\63\u0502\n\63\3\63\5\63\u0505\n\63"+
		"\3\63\3\63\5\63\u0509\n\63\3\63\5\63\u050c\n\63\3\63\5\63\u050f\n\63\3"+
		"\63\5\63\u0512\n\63\3\64\3\64\3\64\3\64\3\64\7\64\u0519\n\64\f\64\16\64"+
		"\u051c\13\64\3\65\3\65\3\65\3\65\3\65\7\65\u0523\n\65\f\65\16\65\u0526"+
		"\13\65\3\66\3\66\5\66\u052a\n\66\3\67\3\67\3\67\5\67\u052f\n\67\3\67\3"+
		"\67\3\67\3\67\3\67\38\38\38\38\38\38\38\58\u053d\n8\39\39\39\39\39\39"+
		"\39\59\u0546\n9\3:\3:\3;\3;\3;\3;\7;\u054e\n;\f;\16;\u0551\13;\3;\3;\3"+
		"<\3<\3=\3=\3=\3=\5=\u055b\n=\3>\3>\3?\3?\3@\3@\3@\3@\3@\3@\5@\u0567\n"+
		"@\3A\3A\3B\3B\3B\3B\3B\5B\u0570\nB\3C\3C\3D\3D\3E\3E\3E\2\6\b\n\26^F\2"+
		"\4\6\b\n\f\16\20\22\24\26\30\32\34\36 \"$&(*,.\60\62\64\668:<>@BDFHJL"+
		"NPRTVXZ\\^`bdfhjlnprtvxz|~\u0080\u0082\u0084\u0086\u0088\2\21\3\2\3\4"+
		"\3\2\5\6\4\2$&RR\3\2\7\f\3\2)*\5\2DEWZ\u0080\u0083\3\2$&\3\2\u0087\u0088"+
		"\3\2\u0089\u008a\3\2BC\4\2AA\u0087\u008b\3\2\u00b4\u00b7\4\2\u00e3\u00e4"+
		"\u00e6\u00e9\4\2<<\u00e6\u00e6\4\2<<\u00c3\u00c3\2\u063c\2\u00a5\3\2\2"+
		"\2\4\u00b4\3\2\2\2\6\u00b9\3\2\2\2\b\u010f\3\2\2\2\n\u0229\3\2\2\2\f\u0235"+
		"\3\2\2\2\16\u023d\3\2\2\2\20\u0264\3\2\2\2\22\u0268\3\2\2\2\24\u026d\3"+
		"\2\2\2\26\u032c\3\2\2\2\30\u0345\3\2\2\2\32\u034e\3\2\2\2\34\u0376\3\2"+
		"\2\2\36\u0382\3\2\2\2 \u0384\3\2\2\2\"\u0394\3\2\2\2$\u039a\3\2\2\2&\u039d"+
		"\3\2\2\2(\u03a6\3\2\2\2*\u03aa\3\2\2\2,\u03b4\3\2\2\2.\u03bc\3\2\2\2\60"+
		"\u03c5\3\2\2\2\62\u03ce\3\2\2\2\64\u03d3\3\2\2\2\66\u03d8\3\2\2\28\u03e1"+
		"\3\2\2\2:\u03e5\3\2\2\2<\u03ed\3\2\2\2>\u03f2\3\2\2\2@\u0404\3\2\2\2B"+
		"\u040c\3\2\2\2D\u0411\3\2\2\2F\u041a\3\2\2\2H\u041c\3\2\2\2J\u041e\3\2"+
		"\2\2L\u0420\3\2\2\2N\u0422\3\2\2\2P\u0424\3\2\2\2R\u0426\3\2\2\2T\u0429"+
		"\3\2\2\2V\u046a\3\2\2\2X\u046c\3\2\2\2Z\u046e\3\2\2\2\\\u047e\3\2\2\2"+
		"^\u0489\3\2\2\2`\u04c0\3\2\2\2b\u04fa\3\2\2\2d\u0511\3\2\2\2f\u0513\3"+
		"\2\2\2h\u051d\3\2\2\2j\u0527\3\2\2\2l\u052e\3\2\2\2n\u053c\3\2\2\2p\u0545"+
		"\3\2\2\2r\u0547\3\2\2\2t\u0549\3\2\2\2v\u0554\3\2\2\2x\u055a\3\2\2\2z"+
		"\u055c\3\2\2\2|\u055e\3\2\2\2~\u0566\3\2\2\2\u0080\u0568\3\2\2\2\u0082"+
		"\u056f\3\2\2\2\u0084\u0571\3\2\2\2\u0086\u0573\3\2\2\2\u0088\u0575\3\2"+
		"\2\2\u008a\u008c\5\4\3\2\u008b\u008a\3\2\2\2\u008b\u008c\3\2\2\2\u008c"+
		"\u0090\3\2\2\2\u008d\u008f\7\u00f9\2\2\u008e\u008d\3\2\2\2\u008f\u0092"+
		"\3\2\2\2\u0090\u008e\3\2\2\2\u0090\u0091\3\2\2\2\u0091\u0096\3\2\2\2\u0092"+
		"\u0090\3\2\2\2\u0093\u0095\7\u00fa\2\2\u0094\u0093\3\2\2\2\u0095\u0098"+
		"\3\2\2\2\u0096\u0094\3\2\2\2\u0096\u0097\3\2\2\2\u0097\u0099\3\2\2\2\u0098"+
		"\u0096\3\2\2\2\u0099\u009b\7\u00f8\2\2\u009a\u008b\3\2\2\2\u009b\u009e"+
		"\3\2\2\2\u009c\u009a\3\2\2\2\u009c\u009d\3\2\2\2\u009d\u00a0\3\2\2\2\u009e"+
		"\u009c\3\2\2\2\u009f\u00a1\5\4\3\2\u00a0\u009f\3\2\2\2\u00a0\u00a1\3\2"+
		"\2\2\u00a1\u00a2\3\2\2\2\u00a2\u00a6\7\2\2\3\u00a3\u00a6\7\u00f9\2\2\u00a4"+
		"\u00a6\7\u00fa\2\2\u00a5\u009c\3\2\2\2\u00a5\u00a3\3\2\2\2\u00a5\u00a4"+
		"\3\2\2\2\u00a6\3\3\2\2\2\u00a7\u00a8\5v<\2\u00a8\u00a9\7\24\2\2\u00a9"+
		"\u00ab\3\2\2\2\u00aa\u00a7\3\2\2\2\u00aa\u00ab\3\2\2\2\u00ab\u00ae\3\2"+
		"\2\2\u00ac\u00af\5\6\4\2\u00ad\u00af\5\26\f\2\u00ae\u00ac\3\2\2\2\u00ae"+
		"\u00ad\3\2\2\2\u00af\u00b5\3\2\2\2\u00b0\u00b1\5v<\2\u00b1\u00b2\7p\2"+
		"\2\u00b2\u00b3\5\6\4\2\u00b3\u00b5\3\2\2\2\u00b4\u00aa\3\2\2\2\u00b4\u00b0"+
		"\3\2\2\2\u00b5\5\3\2\2\2\u00b6\u00ba\5\n\6\2\u00b7\u00ba\5\f\7\2\u00b8"+
		"\u00ba\5\b\5\2\u00b9\u00b6\3\2\2\2\u00b9\u00b7\3\2\2\2\u00b9\u00b8\3\2"+
		"\2\2\u00ba\7\3\2\2\2\u00bb\u00bc\b\5\1\2\u00bc\u0110\5\20\t\2\u00bd\u00be"+
		"\7\'\2\2\u00be\u0110\5\6\4\2\u00bf\u00c0\5\22\n\2\u00c0\u00c5\t\2\2\2"+
		"\u00c1\u00c6\5\b\5\2\u00c2\u00c6\5\n\6\2\u00c3\u00c6\5\f\7\2\u00c4\u00c6"+
		"\5\26\f\2\u00c5\u00c1\3\2\2\2\u00c5\u00c2\3\2\2\2\u00c5\u00c3\3\2\2\2"+
		"\u00c5\u00c4\3\2\2\2\u00c6\u0110\3\2\2\2\u00c7\u00c8\5\22\n\2\u00c8\u00ce"+
		"\t\3\2\2\u00c9\u00cf\5\n\6\2\u00ca\u00cf\5\f\7\2\u00cb\u00cf\5\u0080A"+
		"\2\u00cc\u00cf\5\26\f\2\u00cd\u00cf\5\b\5\2\u00ce\u00c9\3\2\2\2\u00ce"+
		"\u00ca\3\2\2\2\u00ce\u00cb\3\2\2\2\u00ce\u00cc\3\2\2\2\u00ce\u00cd\3\2"+
		"\2\2\u00cf\u0110\3\2\2\2\u00d0\u00d1\5\22\n\2\u00d1\u00d6\t\4\2\2\u00d2"+
		"\u00d7\5\b\5\2\u00d3\u00d7\5\n\6\2\u00d4\u00d7\5\f\7\2\u00d5\u00d7\5\26"+
		"\f\2\u00d6\u00d2\3\2\2\2\u00d6\u00d3\3\2\2\2\u00d6\u00d4\3\2\2\2\u00d6"+
		"\u00d5\3\2\2\2\u00d7\u0110\3\2\2\2\u00d8\u00d9\5\22\n\2\u00d9\u00df\t"+
		"\5\2\2\u00da\u00e0\5\n\6\2\u00db\u00e0\5\f\7\2\u00dc\u00e0\5\u0080A\2"+
		"\u00dd\u00e0\5\26\f\2\u00de\u00e0\5\b\5\2\u00df\u00da\3\2\2\2\u00df\u00db"+
		"\3\2\2\2\u00df\u00dc\3\2\2\2\u00df\u00dd\3\2\2\2\u00df\u00de\3\2\2\2\u00e0"+
		"\u0110\3\2\2\2\u00e1\u00e2\5\22\n\2\u00e2\u00e5\t\6\2\2\u00e3\u00e6\5"+
		"t;\2\u00e4\u00e6\5\u0084C\2\u00e5\u00e3\3\2\2\2\u00e5\u00e4\3\2\2\2\u00e6"+
		"\u0110\3\2\2\2\u00e7\u00ea\5\u0080A\2\u00e8\u00ea\5\26\f\2\u00e9\u00e7"+
		"\3\2\2\2\u00e9\u00e8\3\2\2\2\u00ea\u00eb\3\2\2\2\u00eb\u00ef\t\2\2\2\u00ec"+
		"\u00f0\5\b\5\2\u00ed\u00f0\5\n\6\2\u00ee\u00f0\5\f\7\2\u00ef\u00ec\3\2"+
		"\2\2\u00ef\u00ed\3\2\2\2\u00ef\u00ee\3\2\2\2\u00f0\u0110\3\2\2\2\u00f1"+
		"\u00f4\5\u0080A\2\u00f2\u00f4\5\26\f\2\u00f3\u00f1\3\2\2\2\u00f3\u00f2"+
		"\3\2\2\2\u00f4\u00f5\3\2\2\2\u00f5\u00f9\t\3\2\2\u00f6\u00fa\5\n\6\2\u00f7"+
		"\u00fa\5\f\7\2\u00f8\u00fa\5\b\5\2\u00f9\u00f6\3\2\2\2\u00f9\u00f7\3\2"+
		"\2\2\u00f9\u00f8\3\2\2\2\u00fa\u0110\3\2\2\2\u00fb\u00fe\5\u0080A\2\u00fc"+
		"\u00fe\5\26\f\2\u00fd\u00fb\3\2\2\2\u00fd\u00fc\3\2\2\2\u00fe\u00ff\3"+
		"\2\2\2\u00ff\u0103\t\4\2\2\u0100\u0104\5\b\5\2\u0101\u0104\5\n\6\2\u0102"+
		"\u0104\5\f\7\2\u0103\u0100\3\2\2\2\u0103\u0101\3\2\2\2\u0103\u0102\3\2"+
		"\2\2\u0104\u0110\3\2\2\2\u0105\u0108\5\u0080A\2\u0106\u0108\5\26\f\2\u0107"+
		"\u0105\3\2\2\2\u0107\u0106\3\2\2\2\u0108\u0109\3\2\2\2\u0109\u010d\t\5"+
		"\2\2\u010a\u010e\5\n\6\2\u010b\u010e\5\f\7\2\u010c\u010e\5\b\5\2\u010d"+
		"\u010a\3\2\2\2\u010d\u010b\3\2\2\2\u010d\u010c\3\2\2\2\u010e\u0110\3\2"+
		"\2\2\u010f\u00bb\3\2\2\2\u010f\u00bd\3\2\2\2\u010f\u00bf\3\2\2\2\u010f"+
		"\u00c7\3\2\2\2\u010f\u00d0\3\2\2\2\u010f\u00d8\3\2\2\2\u010f\u00e1\3\2"+
		"\2\2\u010f\u00e9\3\2\2\2\u010f\u00f3\3\2\2\2\u010f\u00fd\3\2\2\2\u010f"+
		"\u0107\3\2\2\2\u0110\u013b\3\2\2\2\u0111\u0112\f\13\2\2\u0112\u0117\t"+
		"\2\2\2\u0113\u0118\5\b\5\2\u0114\u0118\5\n\6\2\u0115\u0118\5\f\7\2\u0116"+
		"\u0118\5\26\f\2\u0117\u0113\3\2\2\2\u0117\u0114\3\2\2\2\u0117\u0115\3"+
		"\2\2\2\u0117\u0116\3\2\2\2\u0118\u013a\3\2\2\2\u0119\u011a\f\n\2\2\u011a"+
		"\u0120\t\3\2\2\u011b\u0121\5\n\6\2\u011c\u0121\5\f\7\2\u011d\u0121\5\u0080"+
		"A\2\u011e\u0121\5\26\f\2\u011f\u0121\5\b\5\2\u0120\u011b\3\2\2\2\u0120"+
		"\u011c\3\2\2\2\u0120\u011d\3\2\2\2\u0120\u011e\3\2\2\2\u0120\u011f\3\2"+
		"\2\2\u0121\u013a\3\2\2\2\u0122\u0123\f\t\2\2\u0123\u0128\t\4\2\2\u0124"+
		"\u0129\5\b\5\2\u0125\u0129\5\n\6\2\u0126\u0129\5\f\7\2\u0127\u0129\5\26"+
		"\f\2\u0128\u0124\3\2\2\2\u0128\u0125\3\2\2\2\u0128\u0126\3\2\2\2\u0128"+
		"\u0127\3\2\2\2\u0129\u013a\3\2\2\2\u012a\u012b\f\b\2\2\u012b\u0131\t\5"+
		"\2\2\u012c\u0132\5\n\6\2\u012d\u0132\5\f\7\2\u012e\u0132\5\u0080A\2\u012f"+
		"\u0132\5\26\f\2\u0130\u0132\5\b\5\2\u0131\u012c\3\2\2\2\u0131\u012d\3"+
		"\2\2\2\u0131\u012e\3\2\2\2\u0131\u012f\3\2\2\2\u0131\u0130\3\2\2\2\u0132"+
		"\u013a\3\2\2\2\u0133\u0134\f\7\2\2\u0134\u0137\t\6\2\2\u0135\u0138\5t"+
		";\2\u0136\u0138\5\u0084C\2\u0137\u0135\3\2\2\2\u0137\u0136\3\2\2\2\u0138"+
		"\u013a\3\2\2\2\u0139\u0111\3\2\2\2\u0139\u0119\3\2\2\2\u0139\u0122\3\2"+
		"\2\2\u0139\u012a\3\2\2\2\u0139\u0133\3\2\2\2\u013a\u013d\3\2\2\2\u013b"+
		"\u0139\3\2\2\2\u013b\u013c\3\2\2\2\u013c\t\3\2\2\2\u013d\u013b\3\2\2\2"+
		"\u013e\u013f\b\6\1\2\u013f\u022a\5x=\2\u0140\u022a\5\16\b\2\u0141\u0142"+
		"\7\17\2\2\u0142\u0143\5x=\2\u0143\u0144\7\20\2\2\u0144\u022a\3\2\2\2\u0145"+
		"\u0146\7\17\2\2\u0146\u0147\5\n\6\2\u0147\u0148\7\20\2\2\u0148\u022a\3"+
		"\2\2\2\u0149\u014a\7\17\2\2\u014a\u014b\5\f\7\2\u014b\u014c\7\20\2\2\u014c"+
		"\u022a\3\2\2\2\u014d\u014e\7\17\2\2\u014e\u014f\5\b\5\2\u014f\u0150\7"+
		"\20\2\2\u0150\u022a\3\2\2\2\u0151\u0152\t\2\2\2\u0152\u022a\5\6\4\2\u0153"+
		"\u0154\7N\2\2\u0154\u0155\7\17\2\2\u0155\u0158\5\6\4\2\u0156\u0157\7\62"+
		"\2\2\u0157\u0159\5\32\16\2\u0158\u0156\3\2\2\2\u0158\u0159\3\2\2\2\u0159"+
		"\u015a\3\2\2\2\u015a\u015b\7\20\2\2\u015b\u022a\3\2\2\2\u015c\u015d\7"+
		"{\2\2\u015d\u015e\7\17\2\2\u015e\u015f\5\6\4\2\u015f\u0160\7\20\2\2\u0160"+
		"\u022a\3\2\2\2\u0161\u0162\7|\2\2\u0162\u0163\7\17\2\2\u0163\u0164\5\6"+
		"\4\2\u0164\u0165\7\20\2\2\u0165\u022a\3\2\2\2\u0166\u0167\7I\2\2\u0167"+
		"\u0168\7\17\2\2\u0168\u0169\5\6\4\2\u0169\u016a\7\20\2\2\u016a\u022a\3"+
		"\2\2\2\u016b\u016c\7`\2\2\u016c\u016d\7\17\2\2\u016d\u016e\5\6\4\2\u016e"+
		"\u016f\7\20\2\2\u016f\u022a\3\2\2\2\u0170\u0171\7K\2\2\u0171\u0172\7\17"+
		"\2\2\u0172\u0173\5\6\4\2\u0173\u0174\7\20\2\2\u0174\u022a\3\2\2\2\u0175"+
		"\u0176\7L\2\2\u0176\u0177\7\17\2\2\u0177\u0178\5\6\4\2\u0178\u0179\7\62"+
		"\2\2\u0179\u017a\5\26\f\2\u017a\u017b\7\20\2\2\u017b\u022a\3\2\2\2\u017c"+
		"\u017d\7M\2\2\u017d\u017e\7\17\2\2\u017e\u0181\5\6\4\2\u017f\u0180\7\62"+
		"\2\2\u0180\u0182\5\32\16\2\u0181\u017f\3\2\2\2\u0181\u0182\3\2\2\2\u0182"+
		"\u0183\3\2\2\2\u0183\u0184\7\20\2\2\u0184\u022a\3\2\2\2\u0185\u0186\7"+
		"O\2\2\u0186\u0187\7\17\2\2\u0187\u0188\5\6\4\2\u0188\u0189\7\62\2\2\u0189"+
		"\u018a\5\26\f\2\u018a\u018b\7\20\2\2\u018b\u022a\3\2\2\2\u018c\u018d\7"+
		"}\2\2\u018d\u018e\7\17\2\2\u018e\u018f\5\6\4\2\u018f\u0190\7\20\2\2\u0190"+
		"\u022a\3\2\2\2\u0191\u0192\7Q\2\2\u0192\u0193\7\17\2\2\u0193\u0194\5\6"+
		"\4\2\u0194\u0195\7\20\2\2\u0195\u022a\3\2\2\2\u0196\u0197\7(\2\2\u0197"+
		"\u0198\7\17\2\2\u0198\u0199\5\6\4\2\u0199\u019a\7\62\2\2\u019a\u019b\5"+
		"\26\f\2\u019b\u019c\7\62\2\2\u019c\u019d\5\26\f\2\u019d\u019e\7\20\2\2"+
		"\u019e\u022a\3\2\2\2\u019f\u01a0\7S\2\2\u01a0\u01a1\7\17\2\2\u01a1\u01a2"+
		"\5\6\4\2\u01a2\u01a3\7\20\2\2\u01a3\u022a\3\2\2\2\u01a4\u01a5\7w\2\2\u01a5"+
		"\u01a6\7\17\2\2\u01a6\u01a7\5\6\4\2\u01a7\u01a8\7\20\2\2\u01a8\u022a\3"+
		"\2\2\2\u01a9\u01aa\7x\2\2\u01aa\u01ab\7\17\2\2\u01ab\u01ac\5\6\4\2\u01ac"+
		"\u01ad\7\20\2\2\u01ad\u022a\3\2\2\2\u01ae\u01af\7T\2\2\u01af\u01b0\7\17"+
		"\2\2\u01b0\u01b1\5\6\4\2\u01b1\u01b2\7\20\2\2\u01b2\u022a\3\2\2\2\u01b3"+
		"\u01b4\7U\2\2\u01b4\u01b5\7\17\2\2\u01b5\u01b6\5\6\4\2\u01b6\u01b7\7\20"+
		"\2\2\u01b7\u022a\3\2\2\2\u01b8\u01b9\7V\2\2\u01b9\u01ba\7\17\2\2\u01ba"+
		"\u01bd\5\6\4\2\u01bb\u01bc\7\62\2\2\u01bc\u01be\5\32\16\2\u01bd\u01bb"+
		"\3\2\2\2\u01bd\u01be\3\2\2\2\u01be\u01c1\3\2\2\2\u01bf\u01c0\7\62\2\2"+
		"\u01c0\u01c2\5\32\16\2\u01c1\u01bf\3\2\2\2\u01c1\u01c2\3\2\2\2\u01c2\u01c3"+
		"\3\2\2\2\u01c3\u01c4\7\20\2\2\u01c4\u022a\3\2\2\2\u01c5\u01c6\7y\2\2\u01c6"+
		"\u01c7\7\17\2\2\u01c7\u01c8\5\6\4\2\u01c8\u01c9\7\62\2\2\u01c9\u01cc\5"+
		"\26\f\2\u01ca\u01cb\7\62\2\2\u01cb\u01cd\5\32\16\2\u01cc\u01ca\3\2\2\2"+
		"\u01cc\u01cd\3\2\2\2\u01cd\u01d0\3\2\2\2\u01ce\u01cf\7\62\2\2\u01cf\u01d1"+
		"\5\32\16\2\u01d0\u01ce\3\2\2\2\u01d0\u01d1\3\2\2\2\u01d1\u01d2\3\2\2\2"+
		"\u01d2\u01d3\7\20\2\2\u01d3\u022a\3\2\2\2\u01d4\u01d5\7z\2\2\u01d5\u01d6"+
		"\7\17\2\2\u01d6\u01d7\5\6\4\2\u01d7\u01d8\7\62\2\2\u01d8\u01db\5\26\f"+
		"\2\u01d9\u01da\7\62\2\2\u01da\u01dc\5\32\16\2\u01db\u01d9\3\2\2\2\u01db"+
		"\u01dc\3\2\2\2\u01dc\u01dd\3\2\2\2\u01dd\u01de\7\20\2\2\u01de\u022a\3"+
		"\2\2\2\u01df\u01e0\7c\2\2\u01e0\u01e1\7\17\2\2\u01e1\u01e2\5\6\4\2\u01e2"+
		"\u01e3\7\62\2\2\u01e3\u01e4\5\26\f\2\u01e4\u01e5\7\20\2\2\u01e5\u022a"+
		"\3\2\2\2\u01e6\u01e7\7+\2\2\u01e7\u01e8\7\17\2\2\u01e8\u01e9\5\6\4\2\u01e9"+
		"\u01ea\7\20\2\2\u01ea\u022a\3\2\2\2\u01eb\u01ec\7e\2\2\u01ec\u01ed\7\17"+
		"\2\2\u01ed\u01ee\5\6\4\2\u01ee\u01f1\7\62\2\2\u01ef\u01f2\5\6\4\2\u01f0"+
		"\u01f2\5\26\f\2\u01f1\u01ef\3\2\2\2\u01f1\u01f0\3\2\2\2\u01f2\u01f3\3"+
		"\2\2\2\u01f3\u01f4\7\20\2\2\u01f4\u022a\3\2\2\2\u01f5\u01f6\7P\2\2\u01f6"+
		"\u01f7\7\17\2\2\u01f7\u01f8\5\6\4\2\u01f8\u01fb\7\62\2\2\u01f9\u01fc\5"+
		"\6\4\2\u01fa\u01fc\5\26\f\2\u01fb\u01f9\3\2\2\2\u01fb\u01fa\3\2\2\2\u01fc"+
		"\u01fd\3\2\2\2\u01fd\u01fe\7\20\2\2\u01fe\u022a\3\2\2\2\u01ff\u0200\7"+
		"\67\2\2\u0200\u0201\7\17\2\2\u0201\u0202\5\6\4\2\u0202\u0203\7\62\2\2"+
		"\u0203\u0206\5\6\4\2\u0204\u0205\7\62\2\2\u0205\u0207\5\u0086D\2\u0206"+
		"\u0204\3\2\2\2\u0206\u0207\3\2\2\2\u0207\u0208\3\2\2\2\u0208\u0209\7\20"+
		"\2\2\u0209\u022a\3\2\2\2\u020a\u020b\7\u0096\2\2\u020b\u020c\7\17\2\2"+
		"\u020c\u020d\5\6\4\2\u020d\u020e\7\20\2\2\u020e\u022a\3\2\2\2\u020f\u0210"+
		"\7\u0097\2\2\u0210\u0211\7\17\2\2\u0211\u0212\5\6\4\2\u0212\u0213\7\20"+
		"\2\2\u0213\u022a\3\2\2\2\u0214\u0215\7\u00c2\2\2\u0215\u0216\7\17\2\2"+
		"\u0216\u0217\5\6\4\2\u0217\u0218\7\20\2\2\u0218\u022a\3\2\2\2\u0219\u021a"+
		"\7\u0098\2\2\u021a\u021b\7\17\2\2\u021b\u021c\5\6\4\2\u021c\u021d\7\62"+
		"\2\2\u021d\u021e\5\26\f\2\u021e\u021f\7\20\2\2\u021f\u022a\3\2\2\2\u0220"+
		"\u0221\7\u0095\2\2\u0221\u0222\7\17\2\2\u0222\u0225\5\6\4\2\u0223\u0224"+
		"\7\62\2\2\u0224\u0226\5\u0088E\2\u0225\u0223\3\2\2\2\u0225\u0226\3\2\2"+
		"\2\u0226\u0227\3\2\2\2\u0227\u0228\7\20\2\2\u0228\u022a\3\2\2\2\u0229"+
		"\u013e\3\2\2\2\u0229\u0140\3\2\2\2\u0229\u0141\3\2\2\2\u0229\u0145\3\2"+
		"\2\2\u0229\u0149\3\2\2\2\u0229\u014d\3\2\2\2\u0229\u0151\3\2\2\2\u0229"+
		"\u0153\3\2\2\2\u0229\u015c\3\2\2\2\u0229\u0161\3\2\2\2\u0229\u0166\3\2"+
		"\2\2\u0229\u016b\3\2\2\2\u0229\u0170\3\2\2\2\u0229\u0175\3\2\2\2\u0229"+
		"\u017c\3\2\2\2\u0229\u0185\3\2\2\2\u0229\u018c\3\2\2\2\u0229\u0191\3\2"+
		"\2\2\u0229\u0196\3\2\2\2\u0229\u019f\3\2\2\2\u0229\u01a4\3\2\2\2\u0229"+
		"\u01a9\3\2\2\2\u0229\u01ae\3\2\2\2\u0229\u01b3\3\2\2\2\u0229\u01b8\3\2"+
		"\2\2\u0229\u01c5\3\2\2\2\u0229\u01d4\3\2\2\2\u0229\u01df\3\2\2\2\u0229"+
		"\u01e6\3\2\2\2\u0229\u01eb\3\2\2\2\u0229\u01f5\3\2\2\2\u0229\u01ff\3\2"+
		"\2\2\u0229\u020a\3\2\2\2\u0229\u020f\3\2\2\2\u0229\u0214\3\2\2\2\u0229"+
		"\u0219\3\2\2\2\u0229\u0220\3\2\2\2\u022a\u0232\3\2\2\2\u022b\u022c\f&"+
		"\2\2\u022c\u022d\7\r\2\2\u022d\u022e\5\36\20\2\u022e\u022f\7\16\2\2\u022f"+
		"\u0231\3\2\2\2\u0230\u022b\3\2\2\2\u0231\u0234\3\2\2\2\u0232\u0230\3\2"+
		"\2\2\u0232\u0233\3\2\2\2\u0233\13\3\2\2\2\u0234\u0232\3\2\2\2\u0235\u0236"+
		"\5\n\6\2\u0236\u0237\7\25\2\2\u0237\u0238\5z>\2\u0238\r\3\2\2\2\u0239"+
		"\u023e\5T+\2\u023a\u023e\5`\61\2\u023b\u023e\5\34\17\2\u023c\u023e\5:"+
		"\36\2\u023d\u0239\3\2\2\2\u023d\u023a\3\2\2\2\u023d\u023b\3\2\2\2\u023d"+
		"\u023c\3\2\2\2\u023e\17\3\2\2\2\u023f\u0240\7\27\2\2\u0240\u0241\5\6\4"+
		"\2\u0241\u0244\7\30\2\2\u0242\u0245\5\6\4\2\u0243\u0245\5\26\f\2\u0244"+
		"\u0242\3\2\2\2\u0244\u0243\3\2\2\2\u0245\u0246\3\2\2\2\u0246\u0249\7\31"+
		"\2\2\u0247\u024a\5\6\4\2\u0248\u024a\5\26\f\2\u0249\u0247\3\2\2\2\u0249"+
		"\u0248\3\2\2\2\u024a\u0265\3\2\2\2\u024b\u024e\7\27\2\2\u024c\u024f\5"+
		"\6\4\2\u024d\u024f\5\26\f\2\u024e\u024c\3\2\2\2\u024e\u024d\3\2\2\2\u024f"+
		"\u0250\3\2\2\2\u0250\u0251\7\30\2\2\u0251\u0252\5\6\4\2\u0252\u0255\7"+
		"\31\2\2\u0253\u0256\5\6\4\2\u0254\u0256\5\26\f\2\u0255\u0253\3\2\2\2\u0255"+
		"\u0254\3\2\2\2\u0256\u0265\3\2\2\2\u0257\u025a\7\27\2\2\u0258\u025b\5"+
		"\6\4\2\u0259\u025b\5\26\f\2\u025a\u0258\3\2\2\2\u025a\u0259\3\2\2\2\u025b"+
		"\u025c\3\2\2\2\u025c\u025f\7\30\2\2\u025d\u0260\5\6\4\2\u025e\u0260\5"+
		"\26\f\2\u025f\u025d\3\2\2\2\u025f\u025e\3\2\2\2\u0260\u0261\3\2\2\2\u0261"+
		"\u0262\7\31\2\2\u0262\u0263\5\6\4\2\u0263\u0265\3\2\2\2\u0264\u023f\3"+
		"\2\2\2\u0264\u024b\3\2\2\2\u0264\u0257\3\2\2\2\u0265\21\3\2\2\2\u0266"+
		"\u0269\5\n\6\2\u0267\u0269\5\f\7\2\u0268\u0266\3\2\2\2\u0268\u0267\3\2"+
		"\2\2\u0269\23\3\2\2\2\u026a\u026b\5\n\6\2\u026b\u026c\7\25\2\2\u026c\u026e"+
		"\3\2\2\2\u026d\u026a\3\2\2\2\u026d\u026e\3\2\2\2\u026e\u026f\3\2\2\2\u026f"+
		"\u0270\5z>\2\u0270\25\3\2\2\2\u0271\u0274\b\f\1\2\u0272\u0275\5\u0080"+
		"A\2\u0273\u0275\5\24\13\2\u0274\u0272\3\2\2\2\u0274\u0273\3\2\2\2\u0275"+
		"\u032d\3\2\2\2\u0276\u0277\7\17\2\2\u0277\u0278\5\26\f\2\u0278\u0279\7"+
		"\20\2\2\u0279\u032d\3\2\2\2\u027a\u032d\5\30\r\2\u027b\u027c\t\2\2\2\u027c"+
		"\u032d\5\24\13\2\u027d\u027e\7\'\2\2\u027e\u032d\5\26\f\"\u027f\u0280"+
		"\7N\2\2\u0280\u0281\7\17\2\2\u0281\u0284\5\26\f\2\u0282\u0283\7\62\2\2"+
		"\u0283\u0285\5\32\16\2\u0284\u0282\3\2\2\2\u0284\u0285\3\2\2\2\u0285\u0286"+
		"\3\2\2\2\u0286\u0287\7\20\2\2\u0287\u032d\3\2\2\2\u0288\u0289\7{\2\2\u0289"+
		"\u028a\7\17\2\2\u028a\u028b\5\26\f\2\u028b\u028c\7\20\2\2\u028c\u032d"+
		"\3\2\2\2\u028d\u028e\7|\2\2\u028e\u028f\7\17\2\2\u028f\u0290\5\26\f\2"+
		"\u0290\u0291\7\20\2\2\u0291\u032d\3\2\2\2\u0292\u0293\7I\2\2\u0293\u0294"+
		"\7\17\2\2\u0294\u0295\5\26\f\2\u0295\u0296\7\20\2\2\u0296\u032d\3\2\2"+
		"\2\u0297\u0298\7`\2\2\u0298\u0299\7\17\2\2\u0299\u029a\5\26\f\2\u029a"+
		"\u029b\7\20\2\2\u029b\u032d\3\2\2\2\u029c\u029d\7K\2\2\u029d\u029e\7\17"+
		"\2\2\u029e\u029f\5\26\f\2\u029f\u02a0\7\20\2\2\u02a0\u032d\3\2\2\2\u02a1"+
		"\u02a2\7L\2\2\u02a2\u02a3\7\17\2\2\u02a3\u02a4\5\26\f\2\u02a4\u02a5\7"+
		"\62\2\2\u02a5\u02a6\5\26\f\2\u02a6\u02a7\7\20\2\2\u02a7\u032d\3\2\2\2"+
		"\u02a8\u02a9\7M\2\2\u02a9\u02aa\7\17\2\2\u02aa\u02ad\5\26\f\2\u02ab\u02ac"+
		"\7\62\2\2\u02ac\u02ae\5\32\16\2\u02ad\u02ab\3\2\2\2\u02ad\u02ae\3\2\2"+
		"\2\u02ae\u02af\3\2\2\2\u02af\u02b0\7\20\2\2\u02b0\u032d\3\2\2\2\u02b1"+
		"\u02b2\7O\2\2\u02b2\u02b3\7\17\2\2\u02b3\u02b4\5\26\f\2\u02b4\u02b5\7"+
		"\62\2\2\u02b5\u02b6\5\26\f\2\u02b6\u02b7\7\20\2\2\u02b7\u032d\3\2\2\2"+
		"\u02b8\u02b9\7}\2\2\u02b9\u02ba\7\17\2\2\u02ba\u02bb\5\26\f\2\u02bb\u02bc"+
		"\7\20\2\2\u02bc\u032d\3\2\2\2\u02bd\u02be\7Q\2\2\u02be\u02bf\7\17\2\2"+
		"\u02bf\u02c0\5\26\f\2\u02c0\u02c1\7\20\2\2\u02c1\u032d\3\2\2\2\u02c2\u02c3"+
		"\7(\2\2\u02c3\u02c4\7\17\2\2\u02c4\u02c5\5\26\f\2\u02c5\u02c6\7\62\2\2"+
		"\u02c6\u02c7\5\26\f\2\u02c7\u02c8\7\62\2\2\u02c8\u02c9\5\26\f\2\u02c9"+
		"\u02ca\7\20\2\2\u02ca\u032d\3\2\2\2\u02cb\u02cc\7S\2\2\u02cc\u02cd\7\17"+
		"\2\2\u02cd\u02ce\5\26\f\2\u02ce\u02cf\7\20\2\2\u02cf\u032d\3\2\2\2\u02d0"+
		"\u02d1\7w\2\2\u02d1\u02d2\7\17\2\2\u02d2\u02d3\5\26\f\2\u02d3\u02d4\7"+
		"\20\2\2\u02d4\u032d\3\2\2\2\u02d5\u02d6\7x\2\2\u02d6\u02d7\7\17\2\2\u02d7"+
		"\u02d8\5\26\f\2\u02d8\u02d9\7\20\2\2\u02d9\u032d\3\2\2\2\u02da\u02db\7"+
		"T\2\2\u02db\u02dc\7\17\2\2\u02dc\u02dd\5\26\f\2\u02dd\u02de\7\20\2\2\u02de"+
		"\u032d\3\2\2\2\u02df\u02e0\7U\2\2\u02e0\u02e1\7\17\2\2\u02e1\u02e2\5\26"+
		"\f\2\u02e2\u02e3\7\20\2\2\u02e3\u032d\3\2\2\2\u02e4\u02e5\7V\2\2\u02e5"+
		"\u02e6\7\17\2\2\u02e6\u02e9\5\26\f\2\u02e7\u02e8\7\62\2\2\u02e8\u02ea"+
		"\5\32\16\2\u02e9\u02e7\3\2\2\2\u02e9\u02ea\3\2\2\2\u02ea\u02ed\3\2\2\2"+
		"\u02eb\u02ec\7\62\2\2\u02ec\u02ee\5\32\16\2\u02ed\u02eb\3\2\2\2\u02ed"+
		"\u02ee\3\2\2\2\u02ee\u02ef\3\2\2\2\u02ef\u02f0\7\20\2\2\u02f0\u032d\3"+
		"\2\2\2\u02f1\u02f2\7y\2\2\u02f2\u02f3\7\17\2\2\u02f3\u02f4\5\26\f\2\u02f4"+
		"\u02f5\7\62\2\2\u02f5\u02f8\5\26\f\2\u02f6\u02f7\7\62\2\2\u02f7\u02f9"+
		"\5\32\16\2\u02f8\u02f6\3\2\2\2\u02f8\u02f9\3\2\2\2\u02f9\u02fc\3\2\2\2"+
		"\u02fa\u02fb\7\62\2\2\u02fb\u02fd\5\32\16\2\u02fc\u02fa\3\2\2\2\u02fc"+
		"\u02fd\3\2\2\2\u02fd\u02fe\3\2\2\2\u02fe\u02ff\7\20\2\2\u02ff\u032d\3"+
		"\2\2\2\u0300\u0301\7z\2\2\u0301\u0302\7\17\2\2\u0302\u0303\5\26\f\2\u0303"+
		"\u0304\7\62\2\2\u0304\u0307\5\26\f\2\u0305\u0306\7\62\2\2\u0306\u0308"+
		"\5\32\16\2\u0307\u0305\3\2\2\2\u0307\u0308\3\2\2\2\u0308\u0309\3\2\2\2"+
		"\u0309\u030a\7\20\2\2\u030a\u032d\3\2\2\2\u030b\u030c\7c\2\2\u030c\u030d"+
		"\7\17\2\2\u030d\u030e\5\26\f\2\u030e\u030f\7\62\2\2\u030f\u0310\5\26\f"+
		"\2\u0310\u0311\7\20\2\2\u0311\u032d\3\2\2\2\u0312\u0313\7+\2\2\u0313\u0314"+
		"\7\17\2\2\u0314\u0315\5\26\f\2\u0315\u0316\7\20\2\2\u0316\u032d\3\2\2"+
		"\2\u0317\u0318\7e\2\2\u0318\u0319\7\17\2\2\u0319\u031a\5\26\f\2\u031a"+
		"\u031b\7\62\2\2\u031b\u031c\5\26\f\2\u031c\u031d\7\20\2\2\u031d\u032d"+
		"\3\2\2\2\u031e\u031f\7P\2\2\u031f\u0320\7\17\2\2\u0320\u0321\5\26\f\2"+
		"\u0321\u0322\7\62\2\2\u0322\u0323\5\26\f\2\u0323\u0324\7\20\2\2\u0324"+
		"\u032d\3\2\2\2\u0325\u0326\7\u00c2\2\2\u0326\u0328\7\17\2\2\u0327\u0329"+
		"\5\26\f\2\u0328\u0327\3\2\2\2\u0328\u0329\3\2\2\2\u0329\u032a\3\2\2\2"+
		"\u032a\u032d\7\20\2\2\u032b\u032d\7\34\2\2\u032c\u0271\3\2\2\2\u032c\u0276"+
		"\3\2\2\2\u032c\u027a\3\2\2\2\u032c\u027b\3\2\2\2\u032c\u027d\3\2\2\2\u032c"+
		"\u027f\3\2\2\2\u032c\u0288\3\2\2\2\u032c\u028d\3\2\2\2\u032c\u0292\3\2"+
		"\2\2\u032c\u0297\3\2\2\2\u032c\u029c\3\2\2\2\u032c\u02a1\3\2\2\2\u032c"+
		"\u02a8\3\2\2\2\u032c\u02b1\3\2\2\2\u032c\u02b8\3\2\2\2\u032c\u02bd\3\2"+
		"\2\2\u032c\u02c2\3\2\2\2\u032c\u02cb\3\2\2\2\u032c\u02d0\3\2\2\2\u032c"+
		"\u02d5\3\2\2\2\u032c\u02da\3\2\2\2\u032c\u02df\3\2\2\2\u032c\u02e4\3\2"+
		"\2\2\u032c\u02f1\3\2\2\2\u032c\u0300\3\2\2\2\u032c\u030b\3\2\2\2\u032c"+
		"\u0312\3\2\2\2\u032c\u0317\3\2\2\2\u032c\u031e\3\2\2\2\u032c\u0325\3\2"+
		"\2\2\u032c\u032b\3\2\2\2\u032d\u0342\3\2\2\2\u032e\u032f\f!\2\2\u032f"+
		"\u0330\t\3\2\2\u0330\u0341\5\26\f\"\u0331\u0332\f \2\2\u0332\u0333\t\2"+
		"\2\2\u0333\u0341\5\26\f!\u0334\u0335\f\37\2\2\u0335\u0336\t\5\2\2\u0336"+
		"\u0341\5\26\f \u0337\u0338\f\36\2\2\u0338\u0339\t\4\2\2\u0339\u0341\5"+
		"\26\f\37\u033a\u033b\f\35\2\2\u033b\u033e\t\6\2\2\u033c\u033f\5t;\2\u033d"+
		"\u033f\5\u0084C\2\u033e\u033c\3\2\2\2\u033e\u033d\3\2\2\2\u033f\u0341"+
		"\3\2\2\2\u0340\u032e\3\2\2\2\u0340\u0331\3\2\2\2\u0340\u0334\3\2\2\2\u0340"+
		"\u0337\3\2\2\2\u0340\u033a\3\2\2\2\u0341\u0344\3\2\2\2\u0342\u0340\3\2"+
		"\2\2\u0342\u0343\3\2\2\2\u0343\27\3\2\2\2\u0344\u0342\3\2\2\2\u0345\u0346"+
		"\7\27\2\2\u0346\u0347\5\26\f\2\u0347\u0348\7\30\2\2\u0348\u0349\5\26\f"+
		"\2\u0349\u034a\7\31\2\2\u034a\u034b\5\26\f\2\u034b\31\3\2\2\2\u034c\u034f"+
		"\5\26\f\2\u034d\u034f\7g\2\2\u034e\u034c\3\2\2\2\u034e\u034d\3\2\2\2\u034f"+
		"\33\3\2\2\2\u0350\u0351\7-\2\2\u0351\u0352\7\17\2\2\u0352\u0357\5\6\4"+
		"\2\u0353\u0354\7\62\2\2\u0354\u0356\5\6\4\2\u0355\u0353\3\2\2\2\u0356"+
		"\u0359\3\2\2\2\u0357\u0355\3\2\2\2\u0357\u0358\3\2\2\2\u0358\u035a\3\2"+
		"\2\2\u0359\u0357\3\2\2\2\u035a\u035b\7\20\2\2\u035b\u0377\3\2\2\2\u035c"+
		"\u035d\7/\2\2\u035d\u035e\7\17\2\2\u035e\u035f\5\6\4\2\u035f\u0360\7\62"+
		"\2\2\u0360\u0361\5\6\4\2\u0361\u0362\7\20\2\2\u0362\u0377\3\2\2\2\u0363"+
		"\u0364\7\177\2\2\u0364\u0365\7\17\2\2\u0365\u0366\5\6\4\2\u0366\u0367"+
		"\7\62\2\2\u0367\u0368\5\6\4\2\u0368\u0369\7\20\2\2\u0369\u0377\3\2\2\2"+
		"\u036a\u036b\7\60\2\2\u036b\u036c\7\17\2\2\u036c\u0371\5\6\4\2\u036d\u036e"+
		"\7\62\2\2\u036e\u0370\5\6\4\2\u036f\u036d\3\2\2\2\u0370\u0373\3\2\2\2"+
		"\u0371\u036f\3\2\2\2\u0371\u0372\3\2\2\2\u0372\u0374\3\2\2\2\u0373\u0371"+
		"\3\2\2\2\u0374\u0375\7\20\2\2\u0375\u0377\3\2\2\2\u0376\u0350\3\2\2\2"+
		"\u0376\u035c\3\2\2\2\u0376\u0363\3\2\2\2\u0376\u036a\3\2\2\2\u0377\35"+
		"\3\2\2\2\u0378\u0383\5 \21\2\u0379\u0383\5d\63\2\u037a\u0383\5$\23\2\u037b"+
		"\u0383\5&\24\2\u037c\u0383\5*\26\2\u037d\u0383\5.\30\2\u037e\u0383\5\60"+
		"\31\2\u037f\u0383\5\62\32\2\u0380\u0383\5\64\33\2\u0381\u0383\5\66\34"+
		"\2\u0382\u0378\3\2\2\2\u0382\u0379\3\2\2\2\u0382\u037a\3\2\2\2\u0382\u037b"+
		"\3\2\2\2\u0382\u037c\3\2\2\2\u0382\u037d\3\2\2\2\u0382\u037e\3\2\2\2\u0382"+
		"\u037f\3\2\2\2\u0382\u0380\3\2\2\2\u0382\u0381\3\2\2\2\u0383\37\3\2\2"+
		"\2\u0384\u0385\7=\2\2\u0385\u038a\5\"\22\2\u0386\u0387\7\62\2\2\u0387"+
		"\u0389\5\"\22\2\u0388\u0386\3\2\2\2\u0389\u038c\3\2\2\2\u038a\u0388\3"+
		"\2\2\2\u038a\u038b\3\2\2\2\u038b\u038e\3\2\2\2\u038c\u038a\3\2\2\2\u038d"+
		"\u038f\5Z.\2\u038e\u038d\3\2\2\2\u038e\u038f\3\2\2\2\u038f\u0391\3\2\2"+
		"\2\u0390\u0392\5\\/\2\u0391\u0390\3\2\2\2\u0391\u0392\3\2\2\2\u0392!\3"+
		"\2\2\2\u0393\u0395\5\u0082B\2\u0394\u0393\3\2\2\2\u0394\u0395\3\2\2\2"+
		"\u0395\u0396\3\2\2\2\u0396\u0397\5z>\2\u0397\u0398\7\24\2\2\u0398\u0399"+
		"\5V,\2\u0399#\3\2\2\2\u039a\u039b\7^\2\2\u039b\u039c\5\26\f\2\u039c%\3"+
		"\2\2\2\u039d\u039e\7\"\2\2\u039e\u03a3\5(\25\2\u039f\u03a0\7\62\2\2\u03a0"+
		"\u03a2\5(\25\2\u03a1\u039f\3\2\2\2\u03a2\u03a5\3\2\2\2\u03a3\u03a1\3\2"+
		"\2\2\u03a3\u03a4\3\2\2\2\u03a4\'\3\2\2\2\u03a5\u03a3\3\2\2\2\u03a6\u03a7"+
		"\5\24\13\2\u03a7\u03a8\78\2\2\u03a8\u03a9\5z>\2\u03a9)\3\2\2\2\u03aa\u03ab"+
		"\7 \2\2\u03ab\u03b0\5,\27\2\u03ac\u03ad\7\62\2\2\u03ad\u03af\5,\27\2\u03ae"+
		"\u03ac\3\2\2\2\u03af\u03b2\3\2\2\2\u03b0\u03ae\3\2\2\2\u03b0\u03b1\3\2"+
		"\2\2\u03b1+\3\2\2\2\u03b2\u03b0\3\2\2\2\u03b3\u03b5\5\u0082B\2\u03b4\u03b3"+
		"\3\2\2\2\u03b4\u03b5\3\2\2\2\u03b5\u03b6\3\2\2\2\u03b6\u03b7\5z>\2\u03b7"+
		"\u03ba\7\24\2\2\u03b8\u03bb\5\26\f\2\u03b9\u03bb\5b\62\2\u03ba\u03b8\3"+
		"\2\2\2\u03ba\u03b9\3\2\2\2\u03bb-\3\2\2\2\u03bc\u03bd\7\37\2\2\u03bd\u03c2"+
		"\5\24\13\2\u03be\u03bf\7\62\2\2\u03bf\u03c1\5\24\13\2\u03c0\u03be\3\2"+
		"\2\2\u03c1\u03c4\3\2\2\2\u03c2\u03c0\3\2\2\2\u03c2\u03c3\3\2\2\2\u03c3"+
		"/\3\2\2\2\u03c4\u03c2\3\2\2\2\u03c5\u03c6\7\36\2\2\u03c6\u03cb\5\24\13"+
		"\2\u03c7\u03c8\7\62\2\2\u03c8\u03ca\5\24\13\2\u03c9\u03c7\3\2\2\2\u03ca"+
		"\u03cd\3\2\2\2\u03cb\u03c9\3\2\2\2\u03cb\u03cc\3\2\2\2\u03cc\61\3\2\2"+
		"\2\u03cd\u03cb\3\2\2\2\u03ce\u03cf\7\u00bd\2\2\u03cf\u03d0\5z>\2\u03d0"+
		"\u03d1\7\62\2\2\u03d1\u03d2\5z>\2\u03d2\63\3\2\2\2\u03d3\u03d4\7\u00be"+
		"\2\2\u03d4\u03d5\5z>\2\u03d5\u03d6\7\62\2\2\u03d6\u03d7\5z>\2\u03d7\65"+
		"\3\2\2\2\u03d8\u03d9\7\u00bf\2\2\u03d9\u03de\58\35\2\u03da\u03db\7\62"+
		"\2\2\u03db\u03dd\58\35\2\u03dc\u03da\3\2\2\2\u03dd\u03e0\3\2\2\2\u03de"+
		"\u03dc\3\2\2\2\u03de\u03df\3\2\2\2\u03df\67\3\2\2\2\u03e0\u03de\3\2\2"+
		"\2\u03e1\u03e2\5\24\13\2\u03e2\u03e3\7\13\2\2\u03e3\u03e4\5\u0080A\2\u03e4"+
		"9\3\2\2\2\u03e5\u03e6\5|?\2\u03e6\u03e7\7\17\2\2\u03e7\u03e9\5<\37\2\u03e8"+
		"\u03ea\5> \2\u03e9\u03e8\3\2\2\2\u03e9\u03ea\3\2\2\2\u03ea\u03eb\3\2\2"+
		"\2\u03eb\u03ec\7\20\2\2\u03ec;\3\2\2\2\u03ed\u03ef\5@!\2\u03ee\u03f0\5"+
		"D#\2\u03ef\u03ee\3\2\2\2\u03ef\u03f0\3\2\2\2\u03f0=\3\2\2\2\u03f1\u03f3"+
		"\5N(\2\u03f2\u03f1\3\2\2\2\u03f2\u03f3\3\2\2\2\u03f3\u03fb\3\2\2\2\u03f4"+
		"\u03fc\5F$\2\u03f5\u03fc\5R*\2\u03f6\u03f7\5H%\2\u03f7\u03f9\5Z.\2\u03f8"+
		"\u03fa\5\\/\2\u03f9\u03f8\3\2\2\2\u03f9\u03fa\3\2\2\2\u03fa\u03fc\3\2"+
		"\2\2\u03fb\u03f4\3\2\2\2\u03fb\u03f5\3\2\2\2\u03fb\u03f6\3\2\2\2\u03fb"+
		"\u03fc\3\2\2\2\u03fc\u03ff\3\2\2\2\u03fd\u0400\5J&\2\u03fe\u0400\5L\'"+
		"\2\u03ff\u03fd\3\2\2\2\u03ff\u03fe\3\2\2\2\u03ff\u0400\3\2\2\2\u0400\u0402"+
		"\3\2\2\2\u0401\u0403\5P)\2\u0402\u0401\3\2\2\2\u0402\u0403\3\2\2\2\u0403"+
		"?\3\2\2\2\u0404\u0409\5B\"\2\u0405\u0406\7\62\2\2\u0406\u0408\5B\"\2\u0407"+
		"\u0405\3\2\2\2\u0408\u040b\3\2\2\2\u0409\u0407\3\2\2\2\u0409\u040a\3\2"+
		"\2\2\u040aA\3\2\2\2\u040b\u0409\3\2\2\2\u040c\u040f\5\6\4\2\u040d\u040e"+
		"\7#\2\2\u040e\u0410\5v<\2\u040f\u040d\3\2\2\2\u040f\u0410\3\2\2\2\u0410"+
		"C\3\2\2\2\u0411\u0412\7\32\2\2\u0412\u0417\5z>\2\u0413\u0414\7\62\2\2"+
		"\u0414\u0416\5z>\2\u0415\u0413\3\2\2\2\u0416\u0419\3\2\2\2\u0417\u0415"+
		"\3\2\2\2\u0417\u0418\3\2\2\2\u0418E\3\2\2\2\u0419\u0417\3\2\2\2\u041a"+
		"\u041b\5*\26\2\u041bG\3\2\2\2\u041c\u041d\5 \21\2\u041dI\3\2\2\2\u041e"+
		"\u041f\5.\30\2\u041fK\3\2\2\2\u0420\u0421\5\60\31\2\u0421M\3\2\2\2\u0422"+
		"\u0423\5$\23\2\u0423O\3\2\2\2\u0424\u0425\5&\24\2\u0425Q\3\2\2\2\u0426"+
		"\u0427\7\u00c0\2\2\u0427\u0428\5\26\f\2\u0428S\3\2\2\2\u0429\u042a\5X"+
		"-\2\u042a\u042b\7\17\2\2\u042b\u042c\5\6\4\2\u042c\u042e\5Z.\2\u042d\u042f"+
		"\5\\/\2\u042e\u042d\3\2\2\2\u042e\u042f\3\2\2\2\u042f\u0430\3\2\2\2\u0430"+
		"\u0431\7\20\2\2\u0431U\3\2\2\2\u0432\u0433\7W\2\2\u0433\u0434\7\17\2\2"+
		"\u0434\u0435\5\24\13\2\u0435\u0436\7\20\2\2\u0436\u046b\3\2\2\2\u0437"+
		"\u0438\7X\2\2\u0438\u0439\7\17\2\2\u0439\u043a\5\24\13\2\u043a\u043b\7"+
		"\20\2\2\u043b\u046b\3\2\2\2\u043c\u043d\7Z\2\2\u043d\u043f\7\17\2\2\u043e"+
		"\u0440\5\24\13\2\u043f\u043e\3\2\2\2\u043f\u0440\3\2\2\2\u0440\u0441\3"+
		"\2\2\2\u0441\u046b\7\20\2\2\u0442\u0443\7Y\2\2\u0443\u0444\7\17\2\2\u0444"+
		"\u0445\5\24\13\2\u0445\u0446\7\20\2\2\u0446\u046b\3\2\2\2\u0447\u0448"+
		"\7D\2\2\u0448\u0449\7\17\2\2\u0449\u044a\5\24\13\2\u044a\u044b\7\20\2"+
		"\2\u044b\u046b\3\2\2\2\u044c\u044d\7E\2\2\u044d\u044e\7\17\2\2\u044e\u044f"+
		"\5\24\13\2\u044f\u0450\7\20\2\2\u0450\u046b\3\2\2\2\u0451\u0452\7A\2\2"+
		"\u0452\u0453\7\17\2\2\u0453\u0454\5\24\13\2\u0454\u0455\7\20\2\2\u0455"+
		"\u046b\3\2\2\2\u0456\u0457\7\u0080\2\2\u0457\u0458\7\17\2\2\u0458\u0459"+
		"\5\24\13\2\u0459\u045a\7\20\2\2\u045a\u046b\3\2\2\2\u045b\u045c\7\u0081"+
		"\2\2\u045c\u045d\7\17\2\2\u045d\u045e\5\24\13\2\u045e\u045f\7\20\2\2\u045f"+
		"\u046b\3\2\2\2\u0460\u0461\7\u0082\2\2\u0461\u0462\7\17\2\2\u0462\u0463"+
		"\5\24\13\2\u0463\u0464\7\20\2\2\u0464\u046b\3\2\2\2\u0465\u0466\7\u0083"+
		"\2\2\u0466\u0467\7\17\2\2\u0467\u0468\5\24\13\2\u0468\u0469\7\20\2\2\u0469"+
		"\u046b\3\2\2\2\u046a\u0432\3\2\2\2\u046a\u0437\3\2\2\2\u046a\u043c\3\2"+
		"\2\2\u046a\u0442\3\2\2\2\u046a\u0447\3\2\2\2\u046a\u044c\3\2\2\2\u046a"+
		"\u0451\3\2\2\2\u046a\u0456\3\2\2\2\u046a\u045b\3\2\2\2\u046a\u0460\3\2"+
		"\2\2\u046a\u0465\3\2\2\2\u046bW\3\2\2\2\u046c\u046d\t\7\2\2\u046dY\3\2"+
		"\2\2\u046e\u046f\5~@\2\u046f\u0474\5\24\13\2\u0470\u0471\7\62\2\2\u0471"+
		"\u0473\5\24\13\2\u0472\u0470\3\2\2\2\u0473\u0476\3\2\2\2\u0474\u0472\3"+
		"\2\2\2\u0474\u0475\3\2\2\2\u0475[\3\2\2\2\u0476\u0474\3\2\2\2\u0477\u0478"+
		"\7\u0086\2\2\u0478\u047f\5^\60\2\u0479\u047a\7\u0086\2\2\u047a\u047b\7"+
		"\17\2\2\u047b\u047c\5^\60\2\u047c\u047d\7\20\2\2\u047d\u047f\3\2\2\2\u047e"+
		"\u0477\3\2\2\2\u047e\u0479\3\2\2\2\u047f]\3\2\2\2\u0480\u0481\b\60\1\2"+
		"\u0481\u0482\5\26\f\2\u0482\u0483\t\5\2\2\u0483\u0484\5V,\2\u0484\u048a"+
		"\3\2\2\2\u0485\u0486\5V,\2\u0486\u0487\t\5\2\2\u0487\u0488\5\26\f\2\u0488"+
		"\u048a\3\2\2\2\u0489\u0480\3\2\2\2\u0489\u0485\3\2\2\2\u048a\u0490\3\2"+
		"\2\2\u048b\u048c\f\3\2\2\u048c\u048d\t\b\2\2\u048d\u048f\5^\60\4\u048e"+
		"\u048b\3\2\2\2\u048f\u0492\3\2\2\2\u0490\u048e\3\2\2\2\u0490\u0491\3\2"+
		"\2\2\u0491_\3\2\2\2\u0492\u0490\3\2\2\2\u0493\u0494\5X-\2\u0494\u0495"+
		"\7\17\2\2\u0495\u0496\5\6\4\2\u0496\u0497\7\u008c\2\2\u0497\u0498\7\17"+
		"\2\2\u0498\u0499\5d\63\2\u0499\u049a\7\20\2\2\u049a\u049b\7\20\2\2\u049b"+
		"\u04c1\3\2\2\2\u049c\u049d\t\t\2\2\u049d\u049e\7\17\2\2\u049e\u049f\5"+
		"\6\4\2\u049f\u04a0\7\u008c\2\2\u04a0\u04a1\7\17\2\2\u04a1\u04a2\5d\63"+
		"\2\u04a2\u04a3\7\20\2\2\u04a3\u04a4\7\20\2\2\u04a4\u04c1\3\2\2\2\u04a5"+
		"\u04a6\7\u008b\2\2\u04a6\u04a7\7\17\2\2\u04a7\u04a8\5\6\4\2\u04a8\u04a9"+
		"\7\u008c\2\2\u04a9\u04aa\7\17\2\2\u04aa\u04ab\5f\64\2\u04ab\u04ac\7\20"+
		"\2\2\u04ac\u04ad\7\20\2\2\u04ad\u04c1\3\2\2\2\u04ae\u04af\t\n\2\2\u04af"+
		"\u04b0\7\17\2\2\u04b0\u04b1\5\6\4\2\u04b1\u04b2\7\62\2\2\u04b2\u04b5\5"+
		"\26\f\2\u04b3\u04b4\7\62\2\2\u04b4\u04b6\5\26\f\2\u04b5\u04b3\3\2\2\2"+
		"\u04b5\u04b6\3\2\2\2\u04b6\u04b7\3\2\2\2\u04b7\u04b8\7\u008c\2\2\u04b8"+
		"\u04ba\7\17\2\2\u04b9\u04bb\5f\64\2\u04ba\u04b9\3\2\2\2\u04ba\u04bb\3"+
		"\2\2\2\u04bb\u04bc\3\2\2\2\u04bc\u04bd\5h\65\2\u04bd\u04be\7\20\2\2\u04be"+
		"\u04bf\7\20\2\2\u04bf\u04c1\3\2\2\2\u04c0\u0493\3\2\2\2\u04c0\u049c\3"+
		"\2\2\2\u04c0\u04a5\3\2\2\2\u04c0\u04ae\3\2\2\2\u04c1a\3\2\2\2\u04c2\u04c3"+
		"\5X-\2\u04c3\u04c4\7\17\2\2\u04c4\u04c5\5\24\13\2\u04c5\u04c6\7\u008c"+
		"\2\2\u04c6\u04c7\7\17\2\2\u04c7\u04c8\5d\63\2\u04c8\u04c9\7\20\2\2\u04c9"+
		"\u04ca\7\20\2\2\u04ca\u04fb\3\2\2\2\u04cb\u04cc\t\t\2\2\u04cc\u04cd\7"+
		"\17\2\2\u04cd\u04ce\5\24\13\2\u04ce\u04cf\7\u008c\2\2\u04cf\u04d0\7\17"+
		"\2\2\u04d0\u04d1\5d\63\2\u04d1\u04d2\7\20\2\2\u04d2\u04d3\7\20\2\2\u04d3"+
		"\u04fb\3\2\2\2\u04d4\u04d5\7A\2\2\u04d5\u04d6\7\17\2\2\u04d6\u04d7\7\u008c"+
		"\2\2\u04d7\u04d9\7\17\2\2\u04d8\u04da\5f\64\2\u04d9\u04d8\3\2\2\2\u04d9"+
		"\u04da\3\2\2\2\u04da\u04db\3\2\2\2\u04db\u04dc\5h\65\2\u04dc\u04dd\7\20"+
		"\2\2\u04dd\u04de\7\20\2\2\u04de\u04fb\3\2\2\2\u04df\u04e0\7\u008b\2\2"+
		"\u04e0\u04e1\7\17\2\2\u04e1\u04e2\5\24\13\2\u04e2\u04e3\7\u008c\2\2\u04e3"+
		"\u04e4\7\17\2\2\u04e4\u04e5\5f\64\2\u04e5\u04e6\7\20\2\2\u04e6\u04e7\7"+
		"\20\2\2\u04e7\u04fb\3\2\2\2\u04e8\u04e9\t\n\2\2\u04e9\u04ea\7\17\2\2\u04ea"+
		"\u04eb\5\24\13\2\u04eb\u04ec\7\62\2\2\u04ec\u04ef\5\26\f\2\u04ed\u04ee"+
		"\7\62\2\2\u04ee\u04f0\5\26\f\2\u04ef\u04ed\3\2\2\2\u04ef\u04f0\3\2\2\2"+
		"\u04f0\u04f1\3\2\2\2\u04f1\u04f2\7\u008c\2\2\u04f2\u04f4\7\17\2\2\u04f3"+
		"\u04f5\5f\64\2\u04f4\u04f3\3\2\2\2\u04f4\u04f5\3\2\2\2\u04f5\u04f6\3\2"+
		"\2\2\u04f6\u04f7\5h\65\2\u04f7\u04f8\7\20\2\2\u04f8\u04f9\7\20\2\2\u04f9"+
		"\u04fb\3\2\2\2\u04fa\u04c2\3\2\2\2\u04fa\u04cb\3\2\2\2\u04fa\u04d4\3\2"+
		"\2\2\u04fa\u04df\3\2\2\2\u04fa\u04e8\3\2\2\2\u04fbc\3\2\2\2\u04fc\u04fe"+
		"\5f\64\2\u04fd\u04ff\5h\65\2\u04fe\u04fd\3\2\2\2\u04fe\u04ff\3\2\2\2\u04ff"+
		"\u0501\3\2\2\2\u0500\u0502\5l\67\2\u0501\u0500\3\2\2\2\u0501\u0502\3\2"+
		"\2\2\u0502\u0512\3\2\2\2\u0503\u0505\5f\64\2\u0504\u0503\3\2\2\2\u0504"+
		"\u0505\3\2\2\2\u0505\u0506\3\2\2\2\u0506\u0508\5h\65\2\u0507\u0509\5l"+
		"\67\2\u0508\u0507\3\2\2\2\u0508\u0509\3\2\2\2\u0509\u0512\3\2\2\2\u050a"+
		"\u050c\5f\64\2\u050b\u050a\3\2\2\2\u050b\u050c\3\2\2\2\u050c\u050e\3\2"+
		"\2\2\u050d\u050f\5h\65\2\u050e\u050d\3\2\2\2\u050e\u050f\3\2\2\2\u050f"+
		"\u0510\3\2\2\2\u0510\u0512\5l\67\2\u0511\u04fc\3\2\2\2\u0511\u0504\3\2"+
		"\2\2\u0511\u050b\3\2\2\2\u0512e\3\2\2\2\u0513\u0514\7\u0090\2\2\u0514"+
		"\u0515\7@\2\2\u0515\u051a\5\24\13\2\u0516\u0517\7\62\2\2\u0517\u0519\5"+
		"\24\13\2\u0518\u0516\3\2\2\2\u0519\u051c\3\2\2\2\u051a\u0518\3\2\2\2\u051a"+
		"\u051b\3\2\2\2\u051bg\3\2\2\2\u051c\u051a\3\2\2\2\u051d\u051e\7?\2\2\u051e"+
		"\u051f\7@\2\2\u051f\u0524\5j\66\2\u0520\u0521\7\62\2\2\u0521\u0523\5j"+
		"\66\2\u0522\u0520\3\2\2\2\u0523\u0526\3\2\2\2\u0524\u0522\3\2\2\2\u0524"+
		"\u0525\3\2\2\2\u0525i\3\2\2\2\u0526\u0524\3\2\2\2\u0527\u0529\5\24\13"+
		"\2\u0528\u052a\t\13\2\2\u0529\u0528\3\2\2\2\u0529\u052a\3\2\2\2\u052a"+
		"k\3\2\2\2\u052b\u052c\7k\2\2\u052c\u052f\7\u00af\2\2\u052d\u052f\7\u0092"+
		"\2\2\u052e\u052b\3\2\2\2\u052e\u052d\3\2\2\2\u052f\u0530\3\2\2\2\u0530"+
		"\u0531\7(\2\2\u0531\u0532\5n8\2\u0532\u0533\7$\2\2\u0533\u0534\5p9\2\u0534"+
		"m\3\2\2\2\u0535\u0536\7\u00e3\2\2\u0536\u053d\7\u008d\2\2\u0537\u0538"+
		"\7\u0093\2\2\u0538\u0539\7k\2\2\u0539\u053d\7\u00b0\2\2\u053a\u053b\7"+
		"\u008f\2\2\u053b\u053d\7\u008d\2\2\u053c\u0535\3\2\2\2\u053c\u0537\3\2"+
		"\2\2\u053c\u053a\3\2\2\2\u053do\3\2\2\2\u053e\u053f\7\u00e3\2\2\u053f"+
		"\u0546\7\u008e\2\2\u0540\u0541\7\u0093\2\2\u0541\u0542\7k\2\2\u0542\u0546"+
		"\7\u00b0\2\2\u0543\u0544\7\u008f\2\2\u0544\u0546\7\u008e\2\2\u0545\u053e"+
		"\3\2\2\2\u0545\u0540\3\2\2\2\u0545\u0543\3\2\2\2\u0546q\3\2\2\2\u0547"+
		"\u0548\t\f\2\2\u0548s\3\2\2\2\u0549\u054a\7\21\2\2\u054a\u054f\5\26\f"+
		"\2\u054b\u054c\7\62\2\2\u054c\u054e\5\26\f\2\u054d\u054b\3\2\2\2\u054e"+
		"\u0551\3\2\2\2\u054f\u054d\3\2\2\2\u054f\u0550\3\2\2\2\u0550\u0552\3\2"+
		"\2\2\u0551\u054f\3\2\2\2\u0552\u0553\7\22\2\2\u0553u\3\2\2\2\u0554\u0555"+
		"\7\u00ea\2\2\u0555w\3\2\2\2\u0556\u055b\7\u00ea\2\2\u0557\u0558\7\u00ea"+
		"\2\2\u0558\u0559\7\23\2\2\u0559\u055b\7\u00ea\2\2\u055a\u0556\3\2\2\2"+
		"\u055a\u0557\3\2\2\2\u055by\3\2\2\2\u055c\u055d\7\u00ea\2\2\u055d{\3\2"+
		"\2\2\u055e\u055f\t\r\2\2\u055f}\3\2\2\2\u0560\u0561\7\u0084\2\2\u0561"+
		"\u0567\7@\2\2\u0562\u0563\7\u0084\2\2\u0563\u0567\7\u0085\2\2\u0564\u0565"+
		"\7\u0084\2\2\u0565\u0567\7<\2\2\u0566\u0560\3\2\2\2\u0566\u0562\3\2\2"+
		"\2\u0566\u0564\3\2\2\2\u0567\177\3\2\2\2\u0568\u0569\t\16\2\2\u0569\u0081"+
		"\3\2\2\2\u056a\u0570\7\\\2\2\u056b\u0570\7[\2\2\u056c\u0570\7]\2\2\u056d"+
		"\u056e\7b\2\2\u056e\u0570\7]\2\2\u056f\u056a\3\2\2\2\u056f\u056b\3\2\2"+
		"\2\u056f\u056c\3\2\2\2\u056f\u056d\3\2\2\2\u0570\u0083\3\2\2\2\u0571\u0572"+
		"\7\u00ea\2\2\u0572\u0085\3\2\2\2\u0573\u0574\t\17\2\2\u0574\u0087\3\2"+
		"\2\2\u0575\u0576\t\20\2\2\u0576\u0089\3\2\2\2~\u008b\u0090\u0096\u009c"+
		"\u00a0\u00a5\u00aa\u00ae\u00b4\u00b9\u00c5\u00ce\u00d6\u00df\u00e5\u00e9"+
		"\u00ef\u00f3\u00f9\u00fd\u0103\u0107\u010d\u010f\u0117\u0120\u0128\u0131"+
		"\u0137\u0139\u013b\u0158\u0181\u01bd\u01c1\u01cc\u01d0\u01db\u01f1\u01fb"+
		"\u0206\u0225\u0229\u0232\u023d\u0244\u0249\u024e\u0255\u025a\u025f\u0264"+
		"\u0268\u026d\u0274\u0284\u02ad\u02e9\u02ed\u02f8\u02fc\u0307\u0328\u032c"+
		"\u033e\u0340\u0342\u034e\u0357\u0371\u0376\u0382\u038a\u038e\u0391\u0394"+
		"\u03a3\u03b0\u03b4\u03ba\u03c2\u03cb\u03de\u03e9\u03ef\u03f2\u03f9\u03fb"+
		"\u03ff\u0402\u0409\u040f\u0417\u042e\u043f\u046a\u0474\u047e\u0489\u0490"+
		"\u04b5\u04ba\u04c0\u04d9\u04ef\u04f4\u04fa\u04fe\u0501\u0504\u0508\u050b"+
		"\u050e\u0511\u051a\u0524\u0529\u052e\u053c\u0545\u054f\u055a\u0566\u056f";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}