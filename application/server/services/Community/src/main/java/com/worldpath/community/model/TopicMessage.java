package com.worldpath.community.model;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

import javax.validation.constraints.NotEmpty;

import lombok.Getter;
import lombok.Setter;

import java.util.Date;

@Getter
@Setter
@Document(collection = "TopicMessages")
public class TopicMessage {
    @Id
    private String id;

    @NotEmpty(message = "User ID is required.")
    private String user_id;

    @NotEmpty(message = "Topic ID is required.")
    private String topic_id;

    @NotEmpty(message = "Text is required.")
    private String text;

    private Date updated_at = new Date();
    private Date created_at = new Date();
}
