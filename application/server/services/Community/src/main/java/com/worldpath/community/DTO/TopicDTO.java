package com.worldpath.community.DTO;

import lombok.Getter;
import lombok.Setter;

import java.util.Date;

@Getter
@Setter
public class TopicDTO {
    /**
     * TopicDTO is a DTO for Topic.
     */
    private String id;
    private String title;
    private String description;
    private String user_id;
    private Date updated_at;
    private Date created_at;
}
