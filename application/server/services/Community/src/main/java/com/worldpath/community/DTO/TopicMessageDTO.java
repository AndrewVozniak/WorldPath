package com.worldpath.community.DTO;

import lombok.Getter;
import lombok.Setter;


@Getter
@Setter
public class TopicMessageDTO {
    /**
     * TopicMessageDTO is a DTO for TopicMessage.
     */
    private String id;
    private String user_id;
    private String topic_id;
    private String text;
}
