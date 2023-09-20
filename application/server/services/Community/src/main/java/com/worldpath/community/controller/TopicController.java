package com.worldpath.community.controller;

import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import com.worldpath.community.DTO.TopicDTO;

import com.worldpath.community.service.TopicService;

@RestController
public class TopicController {
    private final TopicService topicService;

    public TopicController(TopicService TopicService) {
        this.topicService = TopicService;
    }

    @PostMapping("/topic")
    public TopicDTO createTopic(@RequestBody TopicDTO topicDTO) {
        return topicService.createTopic(topicDTO);
    }
}
