JsOsaDAS1.001.00bplist00�Vscripto� v a r   a p p   =   A p p l i c a t i o n . c u r r e n t A p p l i c a t i o n ( ) 
 a p p . i n c l u d e S t a n d a r d A d d i t i o n s   =   t r u e  
 a p p . d o S h e l l S c r i p t ( " o p e n   t e l : / / 1 1 7 " ) 
 v a r   s e   =   A p p l i c a t i o n ( " S y s t e m   E v e n t s " ) 
 
 f u n c t i o n   w r a p ( e ) { 
     r e t u r n   e . c l a s s ( )   +   "   -   "   +   e . n a m e ( )   +   "   ( "   +   e . d e s c r i p t i o n ( )   +   " ) " ; 
 } 
 
 f u n c t i o n   p r i n t ( t a r g e t ) { 
     v a r   c h i l d s   =   t a r g e t . u i E l e m e n t s ( ) ; 
 
     c o n s o l e . l o g ( " \ n "   +   w r a p ( t a r g e t )   +   "   h a s   "   +   c h i l d s . l e n g t h   +   "   U I   E l e m e n t s . " ) ; 
 
     c h i l d s . f o r E a c h ( f u n c t i o n ( c h i l d ,   i ) { 
         c o n s o l e . l o g ( " -     "   +   i   +   " :   "   +   w r a p ( c h i l d ) ) 
     } ) ; 
 
     c o n s o l e . l o g ( ) ;   / /e9�L 
 } ; 
 / /   w a i t   a p p l i c a t i o n   u p 
 d e l a y ( 1 ) 
 
 v a r   f a c e t i m e   =   s e . p r o c e s s e s [ " F a c e T i m e " ] ; 
 v a r   b u t t o n   =   f a c e t i m e . w i n d o w s [ 0 ] . b u t t o n s [ 0 ] ; 
 b u t t o n . c l i c k ( ) 
 
                              jscr  ��ޭ