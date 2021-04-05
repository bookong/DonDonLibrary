namespace DonDonLibrary.Chart.Diva.FT
{
    /// <summary>
    /// Project DIVA FT script file.
    /// </summary>
    public class Script
    {
        /// <summary>
        /// Returns the number of parameters based on the input opcode.
        /// </summary>
        public static int GetParameterCount(Opcode opcode)
        {
            switch (opcode)
            {
                case Opcode.END: return 0;
                case Opcode.TIME: return 1;
                case Opcode.MIKU_MOVE: return 4;
                case Opcode.MIKU_ROT: return 2;
                case Opcode.MIKU_DISP: return 2;
                case Opcode.MIKU_SHADOW: return 2;
                case Opcode.TARGET: return 7;
                case Opcode.SET_MOTION: return 4;
                case Opcode.SET_PLAYDATA: return 2;
                case Opcode.EFFECT: return 6;
                case Opcode.FADEIN_FIELD: return 2;
                case Opcode.EFFECT_OFF: return 1;
                case Opcode.SET_CAMERA: return 6;
                case Opcode.DATA_CAMERA: return 2;
                case Opcode.CHANGE_FIELD: return 1;
                case Opcode.HIDE_FIELD: return 1;
                case Opcode.MOVE_FIELD: return 3;
                case Opcode.FADEOUT_FIELD: return 2;
                case Opcode.EYE_ANIM: return 3;
                case Opcode.MOUTH_ANIM: return 5;
                case Opcode.HAND_ANIM: return 5;
                case Opcode.LOOK_ANIM: return 4;
                case Opcode.EXPRESSION: return 4;
                case Opcode.LOOK_CAMERA: return 5;
                case Opcode.LYRIC: return 2;
                case Opcode.MUSIC_PLAY: return 0;
                case Opcode.MODE_SELECT: return 2;
                case Opcode.EDIT_MOTION: return 4;
                case Opcode.BAR_TIME_SET: return 2;
                case Opcode.SHADOWHEIGHT: return 2;
                case Opcode.EDIT_FACE: return 1;
                case Opcode.MOVE_CAMERA: return 21;
                case Opcode.PV_END: return 0;
                case Opcode.SHADOWPOS: return 3;
                case Opcode.EDIT_LYRIC: return 2;
                case Opcode.EDIT_TARGET: return 5;
                case Opcode.EDIT_MOUTH: return 1;
                case Opcode.SET_CHARA: return 1;
                case Opcode.EDIT_MOVE: return 7;
                case Opcode.EDIT_SHADOW: return 1;
                case Opcode.EDIT_EYELID: return 1;
                case Opcode.EDIT_EYE: return 2;
                case Opcode.EDIT_ITEM: return 1;
                case Opcode.EDIT_EFFECT: return 2;
                case Opcode.EDIT_DISP: return 1;
                case Opcode.EDIT_HAND_ANIM: return 2;
                case Opcode.AIM: return 3;
                case Opcode.HAND_ITEM: return 3;
                case Opcode.EDIT_BLUSH: return 1;
                case Opcode.NEAR_CLIP: return 2;
                case Opcode.CLOTH_WET: return 2;
                case Opcode.LIGHT_ROT: return 3;
                case Opcode.SCENE_FADE: return 6;
                case Opcode.TONE_TRANS: return 6;
                case Opcode.SATURATE: return 1;
                case Opcode.FADE_MODE: return 1;
                case Opcode.AUTO_BLINK: return 2;
                case Opcode.PARTS_DISP: return 3;
                case Opcode.TARGET_FLYING_TIME: return 1;
                case Opcode.CHARA_SIZE: return 2;
                case Opcode.CHARA_HEIGHT_ADJUST: return 2;
                case Opcode.ITEM_ANIM: return 4;
                case Opcode.CHARA_POS_ADJUST: return 4;
                case Opcode.SCENE_ROT: return 1;
                case Opcode.MOT_SMOOTH: return 2;
                case Opcode.PV_BRANCH_MODE: return 1;
                case Opcode.DATA_CAMERA_START: return 2;
                case Opcode.MOVIE_PLAY: return 1;
                case Opcode.MOVIE_DISP: return 1;
                case Opcode.WIND: return 3;
                case Opcode.OSAGE_STEP: return 3;
                case Opcode.OSAGE_MV_CCL: return 3;
                case Opcode.CHARA_COLOR: return 2;
                case Opcode.SE_EFFECT: return 1;
                case Opcode.EDIT_MOVE_XYZ: return 9;
                case Opcode.EDIT_EYELID_ANIM: return 3;
                case Opcode.EDIT_INSTRUMENT_ITEM: return 2;
                case Opcode.EDIT_MOTION_LOOP: return 4;
                case Opcode.EDIT_EXPRESSION: return 2;
                case Opcode.EDIT_EYE_ANIM: return 3;
                case Opcode.EDIT_MOUTH_ANIM: return 2;
                case Opcode.EDIT_CAMERA: return 24;
                case Opcode.EDIT_MODE_SELECT: return 1;
                case Opcode.PV_END_FADEOUT: return 2;
                case Opcode.TARGET_FLAG: return 1;
                case Opcode.ITEM_ANIM_ATTACH: return 3;
                case Opcode.SHADOW_RANGE: return 1;
                case Opcode.HAND_SCALE: return 3;
                case Opcode.LIGHT_POS: return 4;
                case Opcode.FACE_TYPE: return 1;
                case Opcode.SHADOW_CAST: return 2;
                case Opcode.EDIT_MOTION_F: return 6;
                case Opcode.FOG: return 3;
                case Opcode.BLOOM: return 2;
                case Opcode.COLOR_COLLE: return 3;
                case Opcode.DOF: return 3;
                case Opcode.CHARA_ALPHA: return 4;
                case Opcode.AOTO_CAP: return 1;
                case Opcode.MAN_CAP: return 1;
                case Opcode.TOON: return 3;
                case Opcode.SHIMMER: return 3;
                case Opcode.ITEM_ALPHA: return 4;
                case Opcode.MOVIE_CUT_CHG: return 2;
                case Opcode.CHARA_LIGHT: return 3;
                case Opcode.STAGE_LIGHT: return 3;
                case Opcode.AGEAGE_CTRL: return 8;
                case Opcode.PSE: return 2;
                default: break;
            }
            return -1;
        }
    }
}