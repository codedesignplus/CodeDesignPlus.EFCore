ï/
oD:\Projects\Sdk\CodeDesignPlus.EFCore\CodeDesignPlus.EFCore\src\CodeDesignPlus.EFCore\Repository\IRepository.cs
	namespace

 	
CodeDesignPlus


 
.

 
EFCore

 
.

  

Repository

  *
{ 
public 

	interface 
IRepository  
<  !
TKey! %
,% &
TUserKey' /
>/ 0
{ 
TContext 

GetContext 
< 
TContext $
>$ %
(% &
)& '
where( -
TContext. 6
:7 8
	DbContext9 B
;B C
DbSet 
< 
TEntity 
> 
	GetEntity  
<  !
TEntity! (
>( )
() *
)* +
where, 1
TEntity2 9
:: ;
class< A
,A B
IEntityBaseC N
<N O
TKeyO S
,S T
TUserKeyU ]
>] ^
;^ _
Task$$ 
<$$ 
TEntity$$ 
>$$ 
CreateAsync$$ !
<$$! "
TEntity$$" )
>$$) *
($$* +
TEntity$$+ 2
entity$$3 9
,$$9 :
CancellationToken$$; L
cancellationToken$$M ^
=$$_ `
default$$a h
)$$h i
where$$j o
TEntity$$p w
:$$x y
class$$z 
,	$$ Ä
IEntityBase
$$Å å
<
$$å ç
TKey
$$ç ë
,
$$ë í
TUserKey
$$ì õ
>
$$õ ú
;
$$ú ù
Task,, 
<,, 
bool,, 
>,, 
UpdateAsync,, 
<,, 
TEntity,, &
>,,& '
(,,' (
TEntity,,( /
entity,,0 6
,,,6 7
CancellationToken,,8 I
cancellationToken,,J [
=,,\ ]
default,,^ e
),,e f
where,,g l
TEntity,,m t
:,,u v
class,,w |
,,,| }
IEntityBase	,,~ â
<
,,â ä
TKey
,,ä é
,
,,é è
TUserKey
,,ê ò
>
,,ò ô
;
,,ô ö
Task44 
<44 
bool44 
>44 
DeleteAsync44 
<44 
TEntity44 &
>44& '
(44' (

Expression44( 2
<442 3
Func443 7
<447 8
TEntity448 ?
,44? @
bool44A E
>44E F
>44F G
	predicate44H Q
,44Q R
CancellationToken44S d
cancellationToken44e v
=44w x
default	44y Ä
)
44Ä Å
where
44Ç á
TEntity
44à è
:
44ê ë
class
44í ó
,
44ó ò
IEntityBase
44ô §
<
44§ •
TKey
44• ©
,
44© ™
TUserKey
44´ ≥
>
44≥ ¥
;
44¥ µ
Task<< 
<<< 
List<< 
<<< 
TEntity<< 
><< 
><< 
CreateRangeAsync<< ,
<<<, -
TEntity<<- 4
><<4 5
(<<5 6
List<<6 :
<<<: ;
TEntity<<; B
><<B C
entities<<D L
,<<L M
CancellationToken<<N _
cancellationToken<<` q
=<<r s
default<<t {
)<<{ |
where	<<} Ç
TEntity
<<É ä
:
<<ã å
class
<<ç í
,
<<í ì
IEntityBase
<<î ü
<
<<ü †
TKey
<<† §
,
<<§ •
TUserKey
<<¶ Æ
>
<<Æ Ø
;
<<Ø ∞
TaskDD 
<DD 
boolDD 
>DD 
UpdateRangeAsyncDD #
<DD# $
TEntityDD$ +
>DD+ ,
(DD, -
ListDD- 1
<DD1 2
TEntityDD2 9
>DD9 :
entitiesDD; C
,DDC D
CancellationTokenDDE V
cancellationTokenDDW h
=DDi j
defaultDDk r
)DDr s
whereDDt y
TEntity	DDz Å
:
DDÇ É
class
DDÑ â
,
DDâ ä
IEntityBase
DDã ñ
<
DDñ ó
TKey
DDó õ
,
DDõ ú
TUserKey
DDù •
>
DD• ¶
;
DD¶ ß
TaskLL 
<LL 
boolLL 
>LL 
DeleteRangeAsyncLL #
<LL# $
TEntityLL$ +
>LL+ ,
(LL, -
ListLL- 1
<LL1 2
TEntityLL2 9
>LL9 :
entitiesLL; C
,LLC D
CancellationTokenLLE V
cancellationTokenLLW h
=LLi j
defaultLLk r
)LLr s
whereLLt y
TEntity	LLz Å
:
LLÇ É
class
LLÑ â
,
LLâ ä
IEntityBase
LLã ñ
<
LLñ ó
TKey
LLó õ
,
LLõ ú
TUserKey
LLù •
>
LL• ¶
;
LL¶ ß
TaskUU 
<UU 
boolUU 
>UU 
ChangeStateAsyncUU #
<UU# $
TEntityUU$ +
>UU+ ,
(UU, -
TKeyUU- 1
idUU2 4
,UU4 5
boolUU6 :
stateUU; @
,UU@ A
CancellationTokenUUB S
cancellationTokenUUT e
=UUf g
defaultUUh o
)UUo p
whereUUq v
TEntityUUw ~
:	UU Ä
class
UUÅ Ü
,
UUÜ á
IEntityBase
UUà ì
<
UUì î
TKey
UUî ò
,
UUò ô
TUserKey
UUö ¢
>
UU¢ £
;
UU£ §
Task^^ 
<^^ 
TResult^^ 
>^^ 
TransactionAsync^^ &
<^^& '
TResult^^' .
>^^. /
(^^/ 0
Func^^0 4
<^^4 5
	DbContext^^5 >
,^^> ?
Task^^@ D
<^^D E
TResult^^E L
>^^L M
>^^M N
process^^O V
,^^V W
IsolationLevel^^X f
	isolation^^g p
=^^q r
IsolationLevel	^^s Å
.
^^Å Ç
ReadUncommitted
^^Ç ë
,
^^ë í
CancellationToken
^^ì §
cancellationToken
^^• ∂
=
^^∑ ∏
default
^^π ¿
)
^^¿ ¡
;
^^¡ ¬
}__ 
}`` ›ù
nD:\Projects\Sdk\CodeDesignPlus.EFCore\CodeDesignPlus.EFCore\src\CodeDesignPlus.EFCore\Repository\Repository.cs
	namespace 	
CodeDesignPlus
 
. 
EFCore 
.  

Repository  *
{ 
public 

abstract 
class 

Repository $
<$ %
TKey% )
,) *
TUserKey+ 3
>3 4
:5 6
IRepository7 B
<B C
TKeyC G
,G H
TUserKeyI Q
>Q R
{ 
	protected 
readonly 
	DbContext $
Context% ,
;, -
public 

Repository 
( 
	DbContext #
context$ +
)+ ,
{ 	
this 
. 
Context 
= 
context "
??# %
throw& +
new, /!
ArgumentNullException0 E
(E F
nameofF L
(L M
contextM T
)T U
)U V
;V W
} 	
public&& 
TContext&& 

GetContext&& "
<&&" #
TContext&&# +
>&&+ ,
(&&, -
)&&- .
where&&/ 4
TContext&&5 =
:&&> ?
	DbContext&&@ I
=>&&J L
(&&M N
TContext&&N V
)&&V W
this&&W [
.&&[ \
Context&&\ c
;&&c d
public-- 
DbSet-- 
<-- 
TEntity-- 
>-- 
	GetEntity-- '
<--' (
TEntity--( /
>--/ 0
(--0 1
)--1 2
where--3 8
TEntity--9 @
:--A B
class--C H
,--H I
IEntityBase--J U
<--U V
TKey--V Z
,--Z [
TUserKey--\ d
>--d e
=>--f h
this--i m
.--m n
Context--n u
.--u v
Set--v y
<--y z
TEntity	--z Å
>
--Å Ç
(
--Ç É
)
--É Ñ
;
--Ñ Ö
public66 
Task66 
<66 
TEntity66 
>66 
CreateAsync66 (
<66( )
TEntity66) 0
>660 1
(661 2
TEntity662 9
entity66: @
,66@ A
CancellationToken66B S
cancellationToken66T e
=66f g
default66h o
)66o p
where66q v
TEntity66w ~
:	66 Ä
class
66Å Ü
,
66Ü á
IEntityBase
66à ì
<
66ì î
TKey
66î ò
,
66ò ô
TUserKey
66ö ¢
>
66¢ £
{77 	
if88 
(88 
entity88 
==88 
null88 
)88 
throw99 
new99 !
ArgumentNullException99 /
(99/ 0
nameof990 6
(996 7
entity997 =
)99= >
)99> ?
;99? @
return;; 
this;; 
.;; 
ProcessCreateAsync;; *
(;;* +
entity;;+ 1
,;;1 2
cancellationToken;;3 D
);;D E
;;;E F
}<< 	
privateEE 
asyncEE 
TaskEE 
<EE 
TEntityEE "
>EE" #
ProcessCreateAsyncEE$ 6
<EE6 7
TEntityEE7 >
>EE> ?
(EE? @
TEntityEE@ G
entityEEH N
,EEN O
CancellationTokenEEP a
cancellationTokenEEb s
)EEs t
whereEEu z
TEntity	EE{ Ç
:
EEÉ Ñ
class
EEÖ ä
,
EEä ã
IEntityBase
EEå ó
<
EEó ò
TKey
EEò ú
,
EEú ù
TUserKey
EEû ¶
>
EE¶ ß
{FF 	
awaitGG 
thisGG 
.GG 
ContextGG 
.GG 
AddAsyncGG '
(GG' (
entityGG( .
)GG. /
;GG/ 0
awaitII 
thisII 
.II 
ContextII 
.II 
SaveChangesAsyncII /
(II/ 0
cancellationTokenII0 A
)IIA B
;IIB C
returnKK 
entityKK 
;KK 
}LL 	
publicUU 
TaskUU 
<UU 
boolUU 
>UU 
UpdateAsyncUU %
<UU% &
TEntityUU& -
>UU- .
(UU. /
TEntityUU/ 6
entityUU7 =
,UU= >
CancellationTokenUU? P
cancellationTokenUUQ b
=UUc d
defaultUUe l
)UUl m
whereUUn s
TEntityUUt {
:UU| }
class	UU~ É
,
UUÉ Ñ
IEntityBase
UUÖ ê
<
UUê ë
TKey
UUë ï
,
UUï ñ
TUserKey
UUó ü
>
UUü †
{VV 	
ifWW 
(WW 
entityWW 
==WW 
nullWW 
)WW 
throwXX 
newXX !
ArgumentNullExceptionXX /
(XX/ 0
nameofXX0 6
(XX6 7
entityXX7 =
)XX= >
)XX> ?
;XX? @
returnZZ 
thisZZ 
.ZZ 
ProcessUpdateAsyncZZ *
(ZZ* +
entityZZ+ 1
,ZZ1 2
cancellationTokenZZ3 D
)ZZD E
;ZZE F
}[[ 	
privatedd 
asyncdd 
Taskdd 
<dd 
booldd 
>dd  
ProcessUpdateAsyncdd! 3
<dd3 4
TEntitydd4 ;
>dd; <
(dd< =
TEntitydd= D
entityddE K
,ddK L
CancellationTokenddM ^
cancellationTokendd_ p
)ddp q
whereddr w
TEntityddx 
:
ddÄ Å
class
ddÇ á
,
ddá à
IEntityBase
ddâ î
<
ddî ï
TKey
ddï ô
,
ddô ö
TUserKey
ddõ £
>
dd£ §
{ee 	
thisff 
.ff 
Contextff 
.ff 
Setff 
<ff 
TEntityff $
>ff$ %
(ff% &
)ff& '
.ff' (
Updateff( .
(ff. /
entityff/ 5
)ff5 6
;ff6 7
thishh 
.hh 
Contexthh 
.hh 
Entryhh 
(hh 
entityhh %
)hh% &
.hh& '
Propertyhh' /
(hh/ 0
nameofhh0 6
(hh6 7
IEntityBasehh7 B
<hhB C
TKeyhhC G
,hhG H
TUserKeyhhI Q
>hhQ R
.hhR S
IdUserCreatorhhS `
)hh` a
)hha b
.hhb c

IsModifiedhhc m
=hhn o
falsehhp u
;hhu v
thisii 
.ii 
Contextii 
.ii 
Entryii 
(ii 
entityii %
)ii% &
.ii& '
Propertyii' /
(ii/ 0
nameofii0 6
(ii6 7
IEntityBaseii7 B
<iiB C
TKeyiiC G
,iiG H
TUserKeyiiI Q
>iiQ R
.iiR S
DateCreatediiS ^
)ii^ _
)ii_ `
.ii` a

IsModifiediia k
=iil m
falseiin s
;iis t
varkk 
successkk 
=kk 
awaitkk 
thiskk  $
.kk$ %
Contextkk% ,
.kk, -
SaveChangesAsynckk- =
(kk= >
cancellationTokenkk> O
)kkO P
>kkQ R
$numkkS T
;kkT U
returnmm 
successmm 
;mm 
}nn 	
publicww 
Taskww 
<ww 
boolww 
>ww 
DeleteAsyncww %
<ww% &
TEntityww& -
>ww- .
(ww. /

Expressionww/ 9
<ww9 :
Funcww: >
<ww> ?
TEntityww? F
,wwF G
boolwwH L
>wwL M
>wwM N
	predicatewwO X
,wwX Y
CancellationTokenwwZ k
cancellationTokenwwl }
=ww~ 
default
wwÄ á
)
wwá à
where
wwâ é
TEntity
wwè ñ
:
wwó ò
class
wwô û
,
wwû ü
IEntityBase
ww† ´
<
ww´ ¨
TKey
ww¨ ∞
,
ww∞ ±
TUserKey
ww≤ ∫
>
ww∫ ª
{xx 	
ifyy 
(yy 
	predicateyy 
==yy 
nullyy !
)yy! "
throwzz 
newzz !
ArgumentNullExceptionzz /
(zz/ 0
nameofzz0 6
(zz6 7
	predicatezz7 @
)zz@ A
)zzA B
;zzB C
return|| 
this|| 
.|| 
ProcessDeleteAsync|| *
(||* +
	predicate||+ 4
,||4 5
cancellationToken||6 G
)||G H
;||H I
}}} 	
private
ÜÜ 
async
ÜÜ 
Task
ÜÜ 
<
ÜÜ 
bool
ÜÜ 
>
ÜÜ   
ProcessDeleteAsync
ÜÜ! 3
<
ÜÜ3 4
TEntity
ÜÜ4 ;
>
ÜÜ; <
(
ÜÜ< =

Expression
ÜÜ= G
<
ÜÜG H
Func
ÜÜH L
<
ÜÜL M
TEntity
ÜÜM T
,
ÜÜT U
bool
ÜÜV Z
>
ÜÜZ [
>
ÜÜ[ \
	predicate
ÜÜ] f
,
ÜÜf g
CancellationToken
ÜÜh y 
cancellationTokenÜÜz ã
)ÜÜã å
whereÜÜç í
TEntityÜÜì ö
:ÜÜõ ú
classÜÜù ¢
,ÜÜ¢ £
IEntityBaseÜÜ§ Ø
<ÜÜØ ∞
TKeyÜÜ∞ ¥
,ÜÜ¥ µ
TUserKeyÜÜ∂ æ
>ÜÜæ ø
{
áá 	
var
àà 
entity
àà 
=
àà 
await
àà 
this
àà #
.
àà# $
Context
àà$ +
.
àà+ ,
Set
àà, /
<
àà/ 0
TEntity
àà0 7
>
àà7 8
(
àà8 9
)
àà9 :
.
àà: ;
Where
àà; @
(
àà@ A
	predicate
ààA J
)
ààJ K
.
ààK L!
FirstOrDefaultAsync
ààL _
(
àà_ `
)
àà` a
;
ààa b
if
ää 
(
ää 
entity
ää 
!=
ää 
null
ää 
)
ää 
{
ãã 
this
åå 
.
åå 
Context
åå 
.
åå 
Set
åå  
<
åå  !
TEntity
åå! (
>
åå( )
(
åå) *
)
åå* +
.
åå+ ,
Remove
åå, 2
(
åå2 3
entity
åå3 9
)
åå9 :
;
åå: ;
return
éé 
await
éé 
this
éé !
.
éé! "
Context
éé" )
.
éé) *
SaveChangesAsync
éé* :
(
éé: ;
cancellationToken
éé; L
)
ééL M
>
ééN O
$num
ééP Q
;
ééQ R
}
èè 
return
ëë 
false
ëë 
;
ëë 
}
íí 	
public
õõ 
async
õõ 
Task
õõ 
<
õõ 
List
õõ 
<
õõ 
TEntity
õõ &
>
õõ& '
>
õõ' (
CreateRangeAsync
õõ) 9
<
õõ9 :
TEntity
õõ: A
>
õõA B
(
õõB C
List
õõC G
<
õõG H
TEntity
õõH O
>
õõO P
entities
õõQ Y
,
õõY Z
CancellationToken
õõ[ l
cancellationToken
õõm ~
=õõ Ä
defaultõõÅ à
)õõà â
whereõõä è
TEntityõõê ó
:õõò ô
classõõö ü
,õõü †
IEntityBaseõõ° ¨
<õõ¨ ≠
TKeyõõ≠ ±
,õõ± ≤
TUserKeyõõ≥ ª
>õõª º
{
úú 	
if
ùù 
(
ùù 
!
ùù 
entities
ùù 
.
ùù 
Any
ùù 
(
ùù 
)
ùù 
)
ùù  
return
ûû 
entities
ûû 
;
ûû  
return
†† 
await
†† %
ProcessCreateRangeAsync
†† 0
(
††0 1
entities
††1 9
,
††9 :
cancellationToken
††; L
)
††L M
;
††M N
}
°° 	
private
™™ 
async
™™ 
Task
™™ 
<
™™ 
List
™™ 
<
™™  
TEntity
™™  '
>
™™' (
>
™™( )%
ProcessCreateRangeAsync
™™* A
<
™™A B
TEntity
™™B I
>
™™I J
(
™™J K
List
™™K O
<
™™O P
TEntity
™™P W
>
™™W X
entities
™™Y a
,
™™a b
CancellationToken
™™c t 
cancellationToken™™u Ü
)™™Ü á
where™™à ç
TEntity™™é ï
:™™ñ ó
class™™ò ù
,™™ù û
IEntityBase™™ü ™
<™™™ ´
TKey™™´ Ø
,™™Ø ∞
TUserKey™™± π
>™™π ∫
{
´´ 	
await
¨¨ 
this
¨¨ 
.
¨¨ 
Context
¨¨ 
.
¨¨ 
AddRangeAsync
¨¨ ,
(
¨¨, -
entities
¨¨- 5
)
¨¨5 6
;
¨¨6 7
await
ÆÆ 
this
ÆÆ 
.
ÆÆ 
Context
ÆÆ 
.
ÆÆ 
SaveChangesAsync
ÆÆ /
(
ÆÆ/ 0
cancellationToken
ÆÆ0 A
)
ÆÆA B
;
ÆÆB C
return
∞∞ 
entities
∞∞ 
;
∞∞ 
}
±± 	
public
∫∫ 
async
∫∫ 
Task
∫∫ 
<
∫∫ 
bool
∫∫ 
>
∫∫ 
UpdateRangeAsync
∫∫  0
<
∫∫0 1
TEntity
∫∫1 8
>
∫∫8 9
(
∫∫9 :
List
∫∫: >
<
∫∫> ?
TEntity
∫∫? F
>
∫∫F G
entities
∫∫H P
,
∫∫P Q
CancellationToken
∫∫R c
cancellationToken
∫∫d u
=
∫∫v w
default
∫∫x 
)∫∫ Ä
where∫∫Å Ü
TEntity∫∫á é
:∫∫è ê
class∫∫ë ñ
,∫∫ñ ó
IEntityBase∫∫ò £
<∫∫£ §
TKey∫∫§ ®
,∫∫® ©
TUserKey∫∫™ ≤
>∫∫≤ ≥
{
ªª 	
if
ºº 
(
ºº 
!
ºº 
entities
ºº 
.
ºº 
Any
ºº 
(
ºº 
)
ºº 
)
ºº  
return
ΩΩ 
false
ΩΩ 
;
ΩΩ 
this
øø 
.
øø 
Context
øø 
.
øø 
UpdateRange
øø $
(
øø$ %
entities
øø% -
)
øø- .
;
øø. /
return
¡¡ 
await
¡¡ 
this
¡¡ 
.
¡¡ 
Context
¡¡ %
.
¡¡% &
SaveChangesAsync
¡¡& 6
(
¡¡6 7
cancellationToken
¡¡7 H
)
¡¡H I
==
¡¡J L
entities
¡¡M U
.
¡¡U V
Count
¡¡V [
;
¡¡[ \
}
¬¬ 	
public
ÀÀ 
async
ÀÀ 
Task
ÀÀ 
<
ÀÀ 
bool
ÀÀ 
>
ÀÀ 
DeleteRangeAsync
ÀÀ  0
<
ÀÀ0 1
TEntity
ÀÀ1 8
>
ÀÀ8 9
(
ÀÀ9 :
List
ÀÀ: >
<
ÀÀ> ?
TEntity
ÀÀ? F
>
ÀÀF G
entities
ÀÀH P
,
ÀÀP Q
CancellationToken
ÀÀR c
cancellationToken
ÀÀd u
=
ÀÀv w
default
ÀÀx 
)ÀÀ Ä
whereÀÀÅ Ü
TEntityÀÀá é
:ÀÀè ê
classÀÀë ñ
,ÀÀñ ó
IEntityBaseÀÀò £
<ÀÀ£ §
TKeyÀÀ§ ®
,ÀÀ® ©
TUserKeyÀÀ™ ≤
>ÀÀ≤ ≥
{
ÃÃ 	
if
ÕÕ 
(
ÕÕ 
!
ÕÕ 
entities
ÕÕ 
.
ÕÕ 
Any
ÕÕ 
(
ÕÕ 
)
ÕÕ 
)
ÕÕ  
return
ŒŒ 
false
ŒŒ 
;
ŒŒ 
this
–– 
.
–– 
Context
–– 
.
–– 
RemoveRange
–– $
(
––$ %
entities
––% -
)
––- .
;
––. /
return
““ 
await
““ 
this
““ 
.
““ 
Context
““ %
.
““% &
SaveChangesAsync
““& 6
(
““6 7
cancellationToken
““7 H
)
““H I
==
““J L
entities
““M U
.
““U V
Count
““V [
;
““[ \
}
”” 	
public
›› 
async
›› 
Task
›› 
<
›› 
bool
›› 
>
›› 
ChangeStateAsync
››  0
<
››0 1
TEntity
››1 8
>
››8 9
(
››9 :
TKey
››: >
id
››? A
,
››A B
bool
››C G
state
››H M
,
››M N
CancellationToken
››O `
cancellationToken
››a r
=
››s t
default
››u |
)
››| }
where››~ É
TEntity››Ñ ã
:››å ç
class››é ì
,››ì î
IEntityBase››ï †
<››† °
TKey››° •
,››• ¶
TUserKey››ß Ø
>››Ø ∞
{
ﬁﬁ 	
var
ﬂﬂ 
entity
ﬂﬂ 
=
ﬂﬂ 
await
ﬂﬂ 
this
ﬂﬂ #
.
ﬂﬂ# $
Context
ﬂﬂ$ +
.
ﬂﬂ+ ,
Set
ﬂﬂ, /
<
ﬂﬂ/ 0
TEntity
ﬂﬂ0 7
>
ﬂﬂ7 8
(
ﬂﬂ8 9
)
ﬂﬂ9 :
.
ﬂﬂ: ;!
FirstOrDefaultAsync
ﬂﬂ; N
(
ﬂﬂN O
x
ﬂﬂO P
=>
ﬂﬂQ S
x
ﬂﬂT U
.
ﬂﬂU V
Id
ﬂﬂV X
.
ﬂﬂX Y
Equals
ﬂﬂY _
(
ﬂﬂ_ `
id
ﬂﬂ` b
)
ﬂﬂb c
)
ﬂﬂc d
;
ﬂﬂd e
if
·· 
(
·· 
entity
·· 
!=
·· 
null
·· 
)
·· 
{
‚‚ 
entity
„„ 
.
„„ 
State
„„ 
=
„„ 
state
„„ $
;
„„$ %
return
ÂÂ 
await
ÂÂ 
this
ÂÂ !
.
ÂÂ! "
Context
ÂÂ" )
.
ÂÂ) *
SaveChangesAsync
ÂÂ* :
(
ÂÂ: ;
cancellationToken
ÂÂ; L
)
ÂÂL M
>
ÂÂN O
$num
ÂÂP Q
;
ÂÂQ R
}
ÊÊ 
return
ËË 
false
ËË 
;
ËË 
}
ÈÈ 	
public
ÛÛ 
async
ÛÛ 
Task
ÛÛ 
<
ÛÛ 
TResult
ÛÛ !
>
ÛÛ! "
TransactionAsync
ÛÛ# 3
<
ÛÛ3 4
TResult
ÛÛ4 ;
>
ÛÛ; <
(
ÛÛ< =
Func
ÛÛ= A
<
ÛÛA B
	DbContext
ÛÛB K
,
ÛÛK L
Task
ÛÛM Q
<
ÛÛQ R
TResult
ÛÛR Y
>
ÛÛY Z
>
ÛÛZ [
process
ÛÛ\ c
,
ÛÛc d
IsolationLevel
ÛÛe s
	isolation
ÛÛt }
=
ÛÛ~ 
IsolationLevelÛÛÄ é
.ÛÛé è
ReadUncommittedÛÛè û
,ÛÛû ü!
CancellationTokenÛÛ† ±!
cancellationTokenÛÛ≤ √
=ÛÛƒ ≈
defaultÛÛ∆ Õ
)ÛÛÕ Œ
{
ÙÙ 	
var
ıı 
strategy
ıı 
=
ıı 
this
ıı 
.
ıı  
Context
ıı  '
.
ıı' (
Database
ıı( 0
.
ıı0 1%
CreateExecutionStrategy
ıı1 H
(
ııH I
)
ııI J
;
ııJ K
return
˜˜ 
await
˜˜ 
strategy
˜˜ !
.
˜˜! "
ExecuteAsync
˜˜" .
(
˜˜. /
async
˜˜/ 4
(
˜˜5 6
cancellation
˜˜6 B
)
˜˜B C
=>
˜˜D F
{
¯¯ 
using
˘˘ 
var
˘˘ 
transaction
˘˘ %
=
˘˘& '
await
˘˘( -
this
˘˘. 2
.
˘˘2 3
Context
˘˘3 :
.
˘˘: ;
Database
˘˘; C
.
˘˘C D#
BeginTransactionAsync
˘˘D Y
(
˘˘Y Z
	isolation
˘˘Z c
,
˘˘c d
cancellation
˘˘e q
)
˘˘q r
;
˘˘r s
var
˚˚ 
result
˚˚ 
=
˚˚ 
await
˚˚ "
process
˚˚# *
(
˚˚* +
this
˚˚+ /
.
˚˚/ 0
Context
˚˚0 7
)
˚˚7 8
;
˚˚8 9
await
˝˝ 
transaction
˝˝ !
.
˝˝! "
CommitAsync
˝˝" -
(
˝˝- .
)
˝˝. /
;
˝˝/ 0
return
ˇˇ 
result
ˇˇ 
;
ˇˇ 
}
ÄÄ 
,
ÄÄ 
cancellationToken
ÄÄ  
)
ÄÄ  !
;
ÄÄ! "
}
ÅÅ 	
}
ÇÇ 
}ÉÉ 