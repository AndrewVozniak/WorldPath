package com.worldpath.community.controller;

import org.springframework.web.bind.annotation.*;

import com.worldpath.community.DTO.TopicDTO;

import com.worldpath.community.service.TopicService;

@RestController
public class TopicController {
    private final TopicService topicService;

    public TopicController(TopicService TopicService) {
        this.topicService = TopicService;
    }

    /**
     * @param topicDTO TopicDTO
     * @param user_id User ID
     * @return TopicDTO
     */
    @PostMapping("/topic")
    public TopicDTO createTopic(@RequestBody TopicDTO topicDTO, @RequestHeader("Userid") String user_id) {
        topicDTO.setUser_id(user_id);

        return topicService.createTopic(topicDTO);
    }



    /**
     * @param id Topic ID
     * @return TopicDTO
     */
    @GetMapping("/topic/{id}")
    public TopicDTO getTopicById(@PathVariable String id) {
        return topicService.getTopicById(id);
    }
}
