package com.worldpath.community.controller;

import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.worldpath.community.model.Topic;

import com.worldpath.community.service.TopicService;

@RestController
public class TopicController {
    private final TopicService topicService;

    public TopicController(TopicService TopicService) {
        this.topicService = TopicService;
    }

    @PostMapping("/topic")
    public Topic createTopic(@RequestParam() String title,
                             @RequestParam() String description,
                             @RequestParam() String user_id) {

        if (title == null || title.isEmpty()) {
            throw new IllegalArgumentException("title is required");
        }

        if (description == null || description.isEmpty()) {
            throw new IllegalArgumentException("description is required");
        }

        if (user_id == null || user_id.isEmpty()) {
            throw new IllegalArgumentException("user_id is required");
        }

        Topic topic = new Topic(title, description, user_id, null, null);

        return topicService.createTopic(topic);
    }
}
