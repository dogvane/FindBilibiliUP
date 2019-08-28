using System.Collections.Generic;

namespace BaiduFaceAI.Entity
{
    /// <summary>
    ///     人脸检测返回结果
    /// </summary>
    public class DetectReturn
    {
        /// <summary>
        ///     错误代码
        /// </summary>
        public int error_code { get; set; }

        /// <summary>
        ///     错误消息
        /// </summary>
        public string error_msg { get; set; }

        /// <summary>
        ///     日志id
        /// </summary>
        public long log_id { get; set; }

        /// <summary>
        ///     时间戳
        /// </summary>
        public int timestamp { get; set; }

        /// <summary>
        ///     ？？
        /// </summary>
        public int cached { get; set; }

        /// <summary>
        ///     最终的结果
        /// </summary>
        public DetectResult result { get; set; }
    }

    /// <summary>
    ///     返回结果
    /// </summary>
    public class DetectResult
    {
        /// <summary>
        ///     目前有多少个脸
        /// </summary>
        public int face_num { get; set; }

        /// <summary>
        /// </summary>
        public List<Face_listItem> face_list { get; set; }
    }


    public class Face_listItem
    {
        /// <summary>
        ///     人脸图片的唯一标识
        /// </summary>
        public string face_token { get; set; }

        /// <summary>
        ///     人脸在图片中的位置
        /// </summary>
        public Location location { get; set; }

        /// <summary>
        ///     人脸置信度，范围【0~1】，代表这是一张人脸的概率，0最小、1最大。
        /// </summary>
        public double face_probability { get; set; }

        /// <summary>
        ///     人脸旋转角度参数
        /// </summary>
        public Angle angle { get; set; }

        /// <summary>
        ///     年龄 ，当face_field包含age时返回
        /// </summary>
        public int age { get; set; }

        /// <summary>
        ///     美丑打分，范围0-100，越大表示越美。当face_fields包含beauty时返回
        /// </summary>
        /// <remarks>
        ///     颜值啊，不知道百度的审美标准是什么
        /// </remarks>
        public double beauty { get; set; }

        /// <summary>
        ///     表情，当 face_field包含expression时返回
        /// </summary>
        public Expression expression { get; set; }

        /// <summary>
        ///     脸型，当face_field包含face_shape时返回
        /// </summary>
        public Face_shape face_shape { get; set; }

        /// <summary>
        ///     性别，face_field包含gender时返回
        /// </summary>
        public Gender gender { get; set; }

        /// <summary>
        ///     是否带眼镜，face_field包含glasses时返回
        /// </summary>
        public Glasses glasses { get; set; }

        /// <summary>
        ///     4个关键点位置，左眼中心、右眼中心、鼻尖、嘴中心。face_field包含landmark时返回
        /// </summary>
        public List<LandmarkItem> landmark { get; set; }

        /// <summary>
        ///     72个特征点位置 face_field包含landmark72时返回
        /// </summary>
        public List<Landmark72Item> landmark72 { get; set; }

        /// <summary>
        ///     150个特征点位置 face_field包含landmark150时返回
        /// </summary>
        public Landmark150 landmark150 { get; set; }

        /// <summary>
        ///     人种 face_field包含race时返回
        /// </summary>
        public Race race { get; set; }

        /// <summary>
        ///     人脸质量信息。face_field包含quality时返回
        /// </summary>
        public Quality quality { get; set; }

        /// <summary>
        ///     双眼状态（睁开/闭合） face_field包含eye_status时返回
        /// </summary>
        public Eye_status eye_status { get; set; }

        /// <summary>
        ///     情绪 face_field包含emotion时返回
        /// </summary>
        public Emotion emotion { get; set; }

        /// <summary>
        ///     真实人脸/卡通人脸 face_field包含face_type时返回
        /// </summary>
        public Face_type face_type { get; set; }
    }

    /// <summary>
    ///     人脸在图片中的位置
    /// </summary>
    public class Location
    {
        /// <summary>
        ///     人脸区域离左边界的距离
        /// </summary>
        public double left { get; set; }

        /// <summary>
        ///     人脸区域离上边界的距离
        /// </summary>
        public double top { get; set; }

        /// <summary>
        ///     人脸区域的宽度
        /// </summary>
        public double width { get; set; }

        /// <summary>
        ///     人脸区域的高度
        /// </summary>
        public double height { get; set; }

        /// <summary>
        ///     人脸框相对于竖直方向的顺时针旋转角，[-180,180]
        /// </summary>
        public double rotation { get; set; }
    }

    /// <summary>
    ///     人脸旋转角度参数
    /// </summary>
    public class Angle
    {
        /// <summary>
        ///     三维旋转之左右旋转角[-90(左), 90(右)]
        /// </summary>
        public double yaw { get; set; }

        /// <summary>
        ///     三维旋转之俯仰角度[-90(上), 90(下)]
        /// </summary>
        public double pitch { get; set; }

        /// <summary>
        ///     平面内旋转角[-180(逆时针), 180(顺时针)]
        /// </summary>
        public double roll { get; set; }
    }

    /// <summary>
    ///     表情，当 face_field包含expression时返回
    /// </summary>
    public class Expression
    {
        /// <summary>
        ///     none:不笑；smile:微笑；laugh:大笑
        /// </summary>
        public string type { get; set; }

        /// <summary>
        ///     表情置信度，范围【0~1】，0最小、1最大。
        /// </summary>
        public double probability { get; set; }
    }

    /// <summary>
    ///     脸型，当face_field包含face_shape时返回
    /// </summary>
    public class Face_shape
    {
        /// <summary>
        ///     正方形 triangle:三角形 oval: 椭圆 heart: 心形 round: 圆形
        /// </summary>
        public string type { get; set; }

        /// <summary>
        ///     置信度，范围【0~1】，代表这是人脸形状判断正确的概率，0最小、1最大。
        /// </summary>
        public double probability { get; set; }
    }

    /// <summary>
    ///     性别，face_field包含gender时返回
    /// </summary>
    public class Gender
    {
        /// <summary>
        ///     male:男性 female:女性
        /// </summary>
        public string type { get; set; }

        /// <summary>
        ///     性别置信度，范围【0~1】，0代表概率最小、1代表最大。
        /// </summary>
        public double probability { get; set; }
    }

    /// <summary>
    ///     是否带眼镜，face_field包含glasses时返回
    /// </summary>
    public class Glasses
    {
        /// <summary>
        ///     none:无眼镜，common:普通眼镜，sun:墨镜
        /// </summary>
        public string type { get; set; }

        /// <summary>
        ///     眼镜置信度，范围【0~1】，0代表概率最小、1代表最大。
        /// </summary>
        public double probability { get; set; }
    }

    /// <summary>
    ///     4个关键点位置，左眼中心、右眼中心、鼻尖、嘴中心。face_field包含landmark时返回
    /// </summary>
    public class LandmarkItem
    {
        /// <summary>
        /// </summary>
        public double x { get; set; }

        /// <summary>
        /// </summary>
        public double y { get; set; }
    }

    /// <summary>
    ///     72个特征点位置 face_field包含landmark72时返回
    /// </summary>
    public class Landmark72Item
    {
        /// <summary>
        /// </summary>
        public double x { get; set; }

        /// <summary>
        /// </summary>
        public double y { get; set; }
    }

    /// <summary>
    ///     150个特征点位置 face_field包含landmark150时返回
    /// </summary>
    public class Landmark150Item
    {
        /// <summary>
        /// </summary>
        public double x { get; set; }

        /// <summary>
        /// </summary>
        public double y { get; set; }
    }

    /// <summary>
    ///     150个特征点位置 face_field包含landmark150时返回
    /// </summary>
    public class Landmark150
    {
        /// <summary>
        /// </summary>
        public Landmark150Item cheek_right_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_right_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_right_5 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_right_7 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_right_9 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_right_11 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item chin_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_left_11 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_left_9 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_left_7 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_left_5 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_left_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_left_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_corner_right { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyelid_upper_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyelid_upper_4 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyelid_upper_6 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_corner_left { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyelid_lower_6 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyelid_lower_4 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyelid_lower_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyeball_center { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_right_corner_right { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_right_upper_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_right_upper_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_right_upper_4 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_right_corner_left { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_right_lower_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_right_lower_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_right_lower_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_corner_right { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyelid_upper_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyelid_upper_4 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyelid_upper_6 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_corner_left { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyelid_lower_6 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyelid_lower_4 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyelid_lower_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyeball_center { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_left_corner_right { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_left_upper_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_left_upper_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_left_upper_4 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_left_corner_left { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_left_lower_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_left_lower_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_left_lower_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_right_contour_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_right_contour_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_right_contour_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_right_contour_4 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_right_contour_6 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_left_contour_6 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_left_contour_4 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_left_contour_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_left_contour_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_left_contour_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_tip { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_corner_right_outer { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_outer_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_outer_6 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_outer_9 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_corner_left_outer { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_outer_9 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_outer_6 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_outer_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_inner_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_inner_6 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_inner_9 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_inner_9 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_inner_6 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_inner_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_right_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_right_4 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_right_6 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_right_8 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_right_10 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item chin_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item chin_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_left_10 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_left_8 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_left_6 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_left_4 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item cheek_left_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_right_upper_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_right_upper_5 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_left_upper_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eyebrow_left_upper_5 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyelid_upper_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyelid_upper_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyelid_upper_5 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyelid_upper_7 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyelid_lower_7 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyelid_lower_5 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyelid_lower_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyelid_lower_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyeball_right { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_right_eyeball_left { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyelid_upper_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyelid_upper_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyelid_upper_5 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyelid_upper_7 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyelid_lower_7 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyelid_lower_5 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyelid_lower_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyelid_lower_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyeball_right { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item eye_left_eyeball_left { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_bridge_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_bridge_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_bridge_3 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_right_contour_5 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_right_contour_7 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_left_contour_7 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_left_contour_5 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item nose_middle_contour { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_corner_right_inner { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_corner_left_inner { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_outer_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_outer_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_outer_4 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_outer_5 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_outer_7 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_outer_8 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_outer_10 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_outer_11 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_outer_11 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_outer_10 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_outer_8 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_outer_7 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_outer_5 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_outer_4 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_outer_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_outer_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_inner_1 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_inner_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_inner_4 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_inner_5 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_inner_7 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_inner_8 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_inner_10 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_upper_inner_11 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_inner_11 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_inner_10 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_inner_8 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_inner_7 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_inner_5 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_inner_4 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_inner_2 { get; set; }

        /// <summary>
        /// </summary>
        public Landmark150Item mouth_lip_lower_inner_1 { get; set; }
    }

    /// <summary>
    ///     人种 face_field包含race时返回
    /// </summary>
    public class Race
    {
        /// <summary>
        ///     yellow: 黄种人 white: 白种人 black:黑种人 arabs: 阿拉伯人
        /// </summary>
        public string type { get; set; }

        /// <summary>
        ///     人种置信度，范围【0~1】，0代表概率最小、1代表最大。
        /// </summary>
        public double probability { get; set; }
    }

    /// <summary>
    ///     人脸各部分遮挡的概率，范围[0~1]，0表示完整，1表示不完整
    /// </summary>
    public class Occlusion
    {
        /// <summary>
        ///     左眼遮挡比例，[0-1] ，1表示完全遮挡
        /// </summary>
        public double left_eye { get; set; }

        /// <summary>
        ///     右眼遮挡比例，[0-1] ， 1表示完全遮挡
        /// </summary>
        public double right_eye { get; set; }

        /// <summary>
        ///     鼻子遮挡比例，[0-1] ， 1表示完全遮挡
        /// </summary>
        public double nose { get; set; }

        /// <summary>
        ///     嘴巴遮挡比例，[0-1] ， 1表示完全遮挡
        /// </summary>
        public double mouth { get; set; }

        /// <summary>
        ///     左脸颊遮挡比例，[0-1] ， 1表示完全遮挡
        /// </summary>
        public double left_cheek { get; set; }

        /// <summary>
        ///     右脸颊遮挡比例，[0-1] ， 1表示完全遮挡
        /// </summary>
        public double right_cheek { get; set; }

        /// <summary>
        ///     下巴遮挡比例，，[0-1] ， 1表示完全遮挡
        /// </summary>
        public double chin_contour { get; set; }
    }

    /// <summary>
    ///     人脸质量信息。face_field包含quality时返回
    /// </summary>
    public class Quality
    {
        /// <summary>
        ///     人脸各部分遮挡的概率，范围[0~1]，0表示完整，1表示不完整
        /// </summary>
        public Occlusion occlusion { get; set; }

        /// <summary>
        ///     人脸模糊程度，范围[0~1]，0表示清晰，1表示模糊
        /// </summary>
        public double blur { get; set; }

        /// <summary>
        ///     取值范围在[0~255], 表示脸部区域的光照程度 越大表示光照越好
        /// </summary>
        public double illumination { get; set; }

        /// <summary>
        ///     人脸完整度，0或1, 0为人脸溢出图像边界，1为人脸都在图像边界内
        /// </summary>
        public double completeness { get; set; }
    }

    /// <summary>
    ///     双眼状态（睁开/闭合） face_field包含eye_status时返回
    /// </summary>
    public class Eye_status
    {
        /// <summary>
        ///     左眼状态 [0,1]取值，越接近0闭合的可能性越大
        /// </summary>
        public double left_eye { get; set; }

        /// <summary>
        ///     右眼状态 [0,1]取值，越接近0闭合的可能性越大
        /// </summary>
        public double right_eye { get; set; }
    }

    /// <summary>
    ///     情绪 face_field包含emotion时返回
    /// </summary>
    public class Emotion
    {
        /// <summary>
        ///     angry:愤怒 disgust:厌恶 fear:恐惧 happy:高兴 sad:伤心 surprise:惊讶 neutral:无情绪
        /// </summary>
        public string type { get; set; }

        /// <summary>
        ///     情绪置信度，范围0~1
        /// </summary>
        public double probability { get; set; }
    }

    /// <summary>
    ///     真实人脸/卡通人脸 face_field包含face_type时返回
    /// </summary>
    public class Face_type
    {
        /// <summary>
        ///     human: 真实人脸 cartoon: 卡通人脸
        /// </summary>
        public string type { get; set; }

        /// <summary>
        ///     人脸类型判断正确的置信度，范围【0~1】，0代表概率最小、1代表最大。
        /// </summary>
        public double probability { get; set; }
    }
}
